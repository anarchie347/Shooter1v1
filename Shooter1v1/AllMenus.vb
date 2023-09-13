Module AllMenus
    Public Sub RunMainMenu()
        Form1.Text = "Shooter 1v1 - Main menu"
        Dim Play1v1 As New Button With {
            .Size = New Size(100, 100),
            .BackColor = Color.LimeGreen,
            .Name = "MM_Play1v1",
            .Text = "1v1",
            .Font = New Font("OCR A Extended", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        }
        Form1.Controls.Add(Play1v1)
        Play1v1.CentreAlign
        AddHandler Play1v1.Click, AddressOf Play1v1_Click

    End Sub

    Private Sub Play1v1_Click(sender As Object, e As EventArgs)
        Form1.DisposeAllChildren("MM_")
        Dim newgame As New ShooterGame
        newgame.StartGame()
    End Sub


End Module
