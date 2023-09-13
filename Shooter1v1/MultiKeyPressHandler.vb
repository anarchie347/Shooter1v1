Module MultiKeyPressHandler
    Public KeysCurrentlyDown As List(Of Keys)
    Private OldFormKeyPreview As Boolean

    Public Sub StartHandling()
        AddHandler Form1.KeyDown, AddressOf RunKeyDown
        AddHandler Form1.KeyUp, AddressOf RunKeyUp
        KeysCurrentlyDown = New List(Of Keys)
        OldFormKeyPreview = Form1.KeyPreview
        Form1.KeyPreview = True
    End Sub

    Public Sub StopHandling()
        RemoveHandler Form1.KeyDown, AddressOf RunKeyDown
        RemoveHandler Form1.KeyUp, AddressOf RunKeyUp
        KeysCurrentlyDown.Clear()
        Form1.KeyPreview = OldFormKeyPreview

    End Sub
    Private Sub RunKeyDown(sender As Object, e As KeyEventArgs)

        If Not KeysCurrentlyDown.Contains(e.KeyCode) Then
            KeysCurrentlyDown.Add(e.KeyCode)
            'MsgBox(e.KeyCode)
        End If

        'Form1.Text = e.KeyData.ToString

        'Dim temp As String = ""
        'For Each k In KeysCurrentlyDown
        '    temp &= k.ToString & ","
        'Next
        'If temp = "" Then
        '    temp = "NO KEY DOWN"
        'Else
        '    temp = temp.Remove(temp.Length - 1)
        'End If
        'Form1.Text = temp



    End Sub

    Private Sub RunKeyUp(sender As Object, e As KeyEventArgs)
        KeysCurrentlyDown.Remove(e.KeyCode)

        'Dim temp As String = ""
        'For Each k In KeysCurrentlyDown
        '    temp &= k.ToString & ","
        'Next
        'If temp = "" Then
        '    temp = "NO KEY DOWN"
        'Else
        '    temp = temp.Remove(temp.Length - 1)
        'End If
        'Form1.Text = temp

        'If Not CurrentlyPlaying Then
        '    play()
        'End If
    End Sub
End Module
