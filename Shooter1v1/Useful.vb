Imports System.Runtime.CompilerServices
Public Module Extensions

    <Extension()>
    Sub CentreAlignHorizontal(ByRef obj As Control)
        obj.Left = (Form1.ClientSize.Width \ 2) - (obj.Width \ 2)
    End Sub

    <Extension()>
    Sub CentreAlignVertical(ByRef obj As Control)
        obj.Top = (Form1.ClientSize.Height \ 2) - (obj.Height \ 2)
    End Sub

    <Extension()>
    Sub CentreAlign(ByRef obj As Control)
        obj.CentreAlignHorizontal
        obj.CentreAlignVertical
    End Sub

    <Extension()>
    Sub HideAllChildren(ByRef obj As Form1)
        For Each ctrl As Control In obj.Controls
            ctrl.Hide()
        Next
    End Sub

    <Extension()>
    Sub DisposeAllChildren(ByRef obj As Form1, Optional ByVal Prefix As String = "")
        If Prefix = "" Then
            For i = obj.Controls.Count - 1 To 0 Step -1
                obj.Controls(i).Dispose()

            Next
        Else
            For i = obj.Controls.Count - 1 To 0 Step -1
                If obj.Controls(i).Name.StartsWith(Prefix) Then
                    obj.Controls(i).Dispose()

                End If
            Next
        End If

    End Sub

    <Extension()>
    Function CountOfLetter(ByVal obj As String, ByVal value As Char)
        Dim count As Integer = 0
        For Each letter In obj
            If letter = value Then
                count += 1
            End If
        Next
        Return count
    End Function

End Module

Module Useful
    Public Const Alphabet As String = "abcdefghijklmnopqrstuvwxyz"
End Module