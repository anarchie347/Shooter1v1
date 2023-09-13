Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 546
        Me.Height = 539
        MultiKeyPressHandler.StartHandling()
        Options.SetDefaultOptions()
        AllMenus.RunMainMenu()
    End Sub


    'Required to stop VB intercepting arrow keydown event
    Protected Overrides Function ProcessDialogKey(keyData As Keys) As Boolean
        If keyData = Keys.Up Or keyData = Keys.Down Or keyData = Keys.Left Or keyData = Keys.Right Then
            Return False
        End If
        Return MyBase.ProcessDialogKey(keyData)
    End Function


End Class
