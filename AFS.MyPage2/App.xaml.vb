 Partial Public Class App
    Inherits Application

    public Sub New()
        InitializeComponent()
    End Sub
    
    Private Sub Application_Startup(ByVal o As Object, ByVal e As StartupEventArgs) Handles Me.Startup
        Me.RootVisual = New MainPage()
    End Sub
    
    Private Sub Application_Exit(ByVal o As Object, ByVal e As EventArgs) Handles Me.Exit

    End Sub
    
    Private Sub Application_UnhandledException(ByVal sender As object, ByVal e As ApplicationUnhandledExceptionEventArgs) Handles Me.UnhandledException

        ' Wenn die Anwendung außerhalb des Debuggers ausgeführt wird, melden Sie die Ausnahme mithilfe
        ' des Ausnahmemechanismus des Browsers. In IE wird hier ein gelbes Warnsymbol in der 
        ' Statusleiste angezeigt, Firefox zeigt einen Skriptfehler an.
        If Not System.Diagnostics.Debugger.IsAttached Then

            ' Hinweis: So kann die Anwendung weiterhin ausgeführt werden, nachdem eine Ausnahme ausgelöst, aber nicht
            ' behandelt wurde. 
            ' Bei Produktionsanwendungen sollte diese Fehlerbehandlung durch eine Anwendung ersetzt werden, die 
            ' den Fehler der Website meldet und die Anwendung beendet.
            e.Handled = True
            Deployment.Current.Dispatcher.BeginInvoke(New Action(Of ApplicationUnhandledExceptionEventArgs)(AddressOf ReportErrorToDOM), e)
        End If
    End Sub

   Private Sub ReportErrorToDOM(ByVal e As ApplicationUnhandledExceptionEventArgs)

        Try
            Dim errorMsg As String = e.ExceptionObject.Message + e.ExceptionObject.StackTrace
            errorMsg = errorMsg.Replace(""""c, "'"c).Replace(ChrW(13) & ChrW(10), "\n")

            System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(""Unhandled Error in Silverlight Application " + errorMsg + """);")
        Catch
        
        End Try
    End Sub
    
End Class
