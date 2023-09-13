Public Class ShooterGame
    Private Player1 As Button
    Private Player2 As Button
    Private Player1Lasers As List(Of Button)
    Dim Player2Lasers As List(Of Button)
    Private SpaceShouldStartGame As Boolean
    Private GameTick As Timer
    Private Player1ShootCooldown As Timer
    Private Player2ShootCooldown As Timer
    Private Player1ShootCooldownOver As Boolean
    Private Player2ShootCooldownOver As Boolean
    Private BottomDisplayBackground As Button
    Private r As Random
    Private GameLengthTimer As Timer
    Private GameLength As Integer
    Private GameLengthDisplay As Label
    Private Player1LifeBoxes() As Button
    Private Player2LifeBoxes() As Button
    Private Player1LifeCount As Integer
    Private Player2LifeCount As Integer
    Private Player1Effects As List(Of KeyValuePair(Of String, Integer))
    Private Player2Effects As List(Of KeyValuePair(Of String, Integer))

    Public Sub StartGame()

        Player1Effects = New List(Of KeyValuePair(Of String, Integer))
        Player2Effects = New List(Of KeyValuePair(Of String, Integer))

        Form1.Text = "Shooter 1v1 - PVP"
        r = New Random
        GameTick = New Timer
        GameTick.Interval = 5
        AddHandler GameTick.Tick, AddressOf GameTick_Tick

        GameLength = 0
        GameLengthTimer = New Timer
        GameLengthTimer.Interval = 1000
        AddHandler GameLengthTimer.Tick, AddressOf GameLengthTimer_Tick


        Player1ShootCooldownOver = True
        Player1ShootCooldown = New Timer
        Player1ShootCooldown.Interval = Options.FireDelay
        AddHandler Player1ShootCooldown.Tick, AddressOf Player1ShootCooldown_Tick

        Player2ShootCooldownOver = True
        Player2ShootCooldown = New Timer
        Player2ShootCooldown.Interval = Options.FireDelay
        AddHandler Player2ShootCooldown.Tick, AddressOf Player2ShootCooldown_Tick

        AddHandler Form1.KeyPress, AddressOf Form1_KeyPress

        Player1Lasers = New List(Of Button)
        Player2Lasers = New List(Of Button)



        Form1.Width = 546 'size needed so the width of the usable area is 530
        Form1.Height = 579

        SpaceShouldStartGame = True



        Player1 = New Button With {
            .Size = New Drawing.Size(30, 70),
            .Location = New Point(0, 0),
            .Name = "PVP_Player1",
            .BackColor = Color.Blue
        }

        Player2 = New Button With {
            .Size = New Drawing.Size(30, 70),
            .Location = New Point(500, 0),
            .Name = "PVP_Player2",
            .BackColor = Color.Red
        }
        Form1.Controls.Add(Player1)
        Form1.Controls.Add(Player2)


        AddHandler Player1.Click, AddressOf donothing
        AddHandler Player2.Click, AddressOf donothing




        BottomDisplayBackground = New Button With {
            .Size = New Drawing.Size(Form1.ClientSize.Width, 90),
            .Location = New Point(0, Form1.ClientSize.Height - 90),
            .BackColor = Color.Gray,
            .Name = "PVP_BottomDisplayBackground",
            .Font = New Font("OCR A Extended", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            }
        Form1.Controls.Add(BottomDisplayBackground)

        GameLengthDisplay = New Label With {
            .AutoSize = True,
            .Location = New Point(0, BottomDisplayBackground.Top + 13),
            .Font = New Font("OCR A Extended", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)),
            .Text = "00:00",
            .Name = "PVP_GameLengthDisplay",
            .BackColor = Color.Gray
        }
        Form1.Controls.Add(GameLengthDisplay)
        GameLengthDisplay.CentreAlignHorizontal
        GameLengthDisplay.BringToFront()
        Form1.Text = GameLengthDisplay.Height

        Player1LifeCount = Options.StartingLives
        Player2LifeCount = Options.StartingLives

        ReDim Player1LifeBoxes(4)
        ReDim Player2LifeBoxes(4)

        For i = 0 To Player1LifeBoxes.Length - 1
            Player1LifeBoxes(i) = New Button With {
                .Size = New Drawing.Size(30, 30),
                .BackColor = Color.Blue,
                .FlatStyle = FlatStyle.Flat,
                .Name = "PVP_Player1LifeBoxes" & i,
                .Location = New Point(10 + 40 * i, BottomDisplayBackground.Top + 10)
                }
            Player1LifeBoxes(i).FlatAppearance.BorderColor = Color.Blue
            Form1.Controls.Add(Player1LifeBoxes(i))
            Player1LifeBoxes(i).BringToFront()
            If i >= Player1LifeCount Then
                Player1LifeBoxes(i).Hide()
            End If
        Next

        For i = 0 To Player2LifeBoxes.Length - 1
            Player2LifeBoxes(i) = New Button With {
                .Size = New Drawing.Size(30, 30),
                .BackColor = Color.Red,
                .FlatStyle = FlatStyle.Flat,
                .Name = "PVP_Player2LifeBoxes" & i,
                .Location = New Point(BottomDisplayBackground.Right - 10 - 40 * (i + 1), BottomDisplayBackground.Top + 10)
                }
            Player2LifeBoxes(i).FlatAppearance.BorderColor = Color.Red
            Form1.Controls.Add(Player2LifeBoxes(i))
            Player2LifeBoxes(i).BringToFront()
            If i >= Player2LifeCount Then
                Player2LifeBoxes(i).Hide()
            End If
        Next

        Dim PressSpaceToStart As New Label With {
            .Font = New Font("OCR A Extended", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)),
            .AutoSize = True,
            .Text = "Press Space To Start",
            .Name = "PVP_PressSpaceToStart"
        }
        Form1.Controls.Add(PressSpaceToStart)
        PressSpaceToStart.CentreAlign

    End Sub

    Private Sub Play()
        GameTick.Start()
        GameLengthTimer.Start()
        Form1.Controls.Find("PVP_PressSpaceToStart", True).FirstOrDefault.Dispose()
    End Sub

    Private Sub Endgame()
        'Threading.Thread.Sleep(1000)
        Form1.DisposeAllChildren("PVP_")
        GameLengthTimer.Dispose()
        GameTick.Dispose()
    End Sub


    Private Sub donothing()

    End Sub

    Private Sub GameTick_Tick(sender As Object, e As EventArgs)
        'Dim Player1 As Button = Form1.Controls.Find("Player1", True).FirstOrDefault
        'Dim Player2 As Button = Form1.Controls.Find("Player2", True).FirstOrDefault
        '------------------------------------------CONTROLS------------------------------------------
        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.W) Then
            If Player1.Top >= Options.MoveSpeed Then
                Player1.Top -= Options.MoveSpeed
            End If
        End If

        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.S) Then
            If Player1.Bottom <= Form1.ClientSize.Height - 90 - Options.MoveSpeed Then
                Player1.Top += Options.MoveSpeed
            End If
        End If

        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.Up) Then
            If Player2.Top >= 0 Then
                Player2.Top -= Options.MoveSpeed
            End If
        End If

        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.Down) Then
            If Player2.Bottom <= Form1.ClientSize.Height - 90 - Options.MoveSpeed Then
                Player2.Top += Options.MoveSpeed
            End If
        End If

        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.D) And Player1ShootCooldownOver Then
            Player1ShootCooldownOver = False
            Player1ShootCooldown.Start()
            Dim Laser As New Button With {
                .Size = New Point(30, 10),
                .Location = New Point(Player1.Location.X + 30, Player1.Location.Y + ((Player1.Height / 2) - 5)),
                .Name = "PVP_Player1Laser",
                .Tag = Player1Lasers.Count - 1,
                .BackColor = Color.Blue,
                .FlatStyle = FlatStyle.Flat
            }
            Laser.FlatAppearance.BorderColor = Color.Blue

            Form1.Controls.Add(Laser)
            Player1Lasers.Add(Laser)
        End If

        If MultiKeyPressHandler.KeysCurrentlyDown.Contains(Keys.Left) And Player2ShootCooldownOver Then
            Player2ShootCooldownOver = False
            Player2ShootCooldown.Start()
            Dim Laser As New Button With {
                .Size = New Point(30, 10),
                .Location = New Point(Player2.Location.X - 30, Player2.Location.Y + ((Player2.Height / 2) - 5)),
                .Name = "PVP_Player2Laser",
                .Tag = Player2Lasers.Count - 1,
                .BackColor = Color.Red,
                .FlatStyle = FlatStyle.Flat
            }
            Laser.FlatAppearance.BorderColor = Color.Red
            Form1.Controls.Add(Laser)
            Player2Lasers.Add(Laser)
        End If

        '------------------------------------------MOVE LASERS------------------------------------------

        If Player1Lasers.Count > 0 Then
            For i = Player1Lasers.Count - 1 To 0 Step -1
                If Player1Lasers(i).Left + Options.LaserMoveSpeed <= Form1.ClientSize.Width Then
                    Player1Lasers(i).Left += Options.LaserMoveSpeed
                Else
                    Player1Lasers(i).Dispose()
                    Player1Lasers.RemoveAt(i)
                End If

            Next
        End If

        If Player2Lasers.Count > 0 Then
            For i = Player2Lasers.Count - 1 To 0 Step -1
                If Player2Lasers(i).Right - Options.LaserMoveSpeed >= 0 Then
                    Player2Lasers(i).Left -= Options.LaserMoveSpeed
                Else
                    Player2Lasers(i).Dispose()
                    Player2Lasers.RemoveAt(i)
                End If

            Next
        End If

        '------------------------------------------HIT DETECTION------------------------------------------
        If Player1Lasers.Count > 0 Then
            For i = Player1Lasers.Count - 1 To 0 Step -1
                If Player1Lasers(i).Right > Player2.Left And Player1Lasers(i).Left < Player2.Right And Player1Lasers(i).Top < Player2.Bottom And Player1Lasers(i).Bottom > Player2.Top Then
                    Player1Lasers(i).Dispose()
                    Player1Lasers.RemoveAt(i)


                    Player2LifeCount -= 1
                    Player2LifeBoxes(Player2LifeCount).Hide()
                    If Player2LifeCount = 0 Then

                        WinAnimation("Player 1", Color.Blue)
                        SpaceShouldStartGame = False
                        AddHandler Form1.KeyPress, AddressOf Form1_KeyPress

                    End If
                End If
            Next
        End If

        If Player2Lasers.Count > 0 Then
            For i = Player2Lasers.Count - 1 To 0 Step -1
                If Player2Lasers(i).Right > Player1.Left And Player2Lasers(i).Left < Player1.Right And Player2Lasers(i).Top < Player1.Bottom And Player2Lasers(i).Bottom > Player1.Top Then
                    Player2Lasers(i).Dispose()
                    Player2Lasers.RemoveAt(i)


                    Player1LifeCount -= 1
                    Player1LifeBoxes(Player1LifeCount).Hide()
                    If Player1LifeCount = 0 Then
                        WinAnimation("Player 2", Color.Red)
                        SpaceShouldStartGame = False

                        AddHandler Form1.KeyPress, AddressOf Form1_KeyPress
                    End If
                End If
            Next
        End If


        '------------------------------------------Action Box------------------------------------------
        'If r.Next(0, 100) = 0 Then
        '    'MsgBox("ACTION")
        '    CreateActionBox()
        'End If

        For i = Form1.Controls.Count - 1 To 0 Step -1
            If Form1.Controls(i).Name.StartsWith("PVP_ActionBox") Then
                If Form1.Controls(i).Tag > 1 Then
                    Form1.Controls(i).Tag -= 1

                    'Hit detection
                    If Player1Lasers.Count > 0 Then
                        For j = Player1Lasers.Count - 1 To 0 Step -1
                            If Player1Lasers(j).Right > Form1.Controls(i).Left And Player1Lasers(j).Left < Form1.Controls(i).Right And Player1Lasers(j).Top < Form1.Controls(i).Bottom And Player1Lasers(j).Bottom > Form1.Controls(i).Top Then
                                Player1Lasers(j).Dispose()
                                Player1Lasers.RemoveAt(j)
                                Form1.Controls(i).Dispose()
                                Player1Effects.Add(New KeyValuePair(Of String, Integer)(Form1.Controls(i).Name.Split.Last, 10000))
                            End If
                        Next
                    End If

                    If Player2Lasers.Count > 0 Then
                        For j = Player2Lasers.Count - 1 To 0 Step -1
                            If Player2Lasers(j).Right > Form1.Controls(i).Left And Player2Lasers(j).Left < Form1.Controls(i).Right And Player2Lasers(j).Top < Form1.Controls(i).Bottom And Player2Lasers(j).Bottom > Form1.Controls(i).Top Then
                                Player2Lasers(j).Dispose()
                                Player2Lasers.RemoveAt(j)
                                Form1.Controls(i).Dispose()
                                Player2Effects.Add(New KeyValuePair(Of String, Integer)(Form1.Controls(i).Name.Split.Last, 10000))
                            End If
                        Next
                    End If
                Else
                    Form1.Controls(i).Dispose()
                End If
            End If
        Next


        '------------------------------------------Effects------------------------------------------
        If Player1Effects.Count > 0 Then
            For i = Player1Effects.Count - 1 To 0 Step -1
                If Player1Effects(i).Value = 1 Then
                    Player1Effects.RemoveAt(i)
                Else
                    Player1Effects(i) = New KeyValuePair(Of String, Integer)(Player1Effects(i).Key, Player1Effects(i).Value - 1)
                End If
            Next
        End If

        If Player2Effects.Count > 0 Then
            For i = Player2Effects.Count - 1 To 0 Step -1
                If Player2Effects(i).Value = 1 Then
                    Player2Effects.RemoveAt(i)
                Else
                    Player2Effects(i) = New KeyValuePair(Of String, Integer)(Player2Effects(i).Key, Player2Effects(i).Value - 1)
                End If
            Next
        End If
    End Sub

    Sub WinAnimation(Winner As String, WinnerColour As Color)
        GameTick.Stop()
        GameLengthTimer.Stop()
        Dim WinScreen As New Button With {
            .Text = Winner & " Wins!",
            .BackColor = WinnerColour,
            .Size = New Size(150, 150),
            .Font = New Font("OCR A Extended", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)),
            .Name = "PVP_WinScreen"
        }
        Form1.Controls.Add(WinScreen)
        WinScreen.CentreAlign

        Dim AnyKeyToContinue As New Button With {
            .Text = "Press any key to continue",
            .Size = New Size(150, 40),
            .Font = New Font("OCR A Extended", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)),
            .Name = "PVP_AnyKeyToContinue",
            .Top = WinScreen.Bottom
        }
        Form1.Controls.Add(AnyKeyToContinue)
        AnyKeyToContinue.CentreAlignHorizontal
    End Sub


    Private Sub Player1ShootCooldown_Tick(sender As Object, e As EventArgs)
        Player1ShootCooldownOver = True
        CType(sender, Timer).Stop()
    End Sub

    Private Sub Player2ShootCooldown_Tick(sender As Object, e As EventArgs)
        Player2ShootCooldownOver = True
        CType(sender, Timer).Stop()
    End Sub

    Private Sub GameLengthTimer_Tick(sender As Object, e As EventArgs)
        GameLength += 1
        GameLengthDisplay.Text = (GameLength \ 60).ToString.PadLeft(2, "0") & ":" & (GameLength Mod 60).ToString.PadLeft(2, "0")
    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If SpaceShouldStartGame Then
            If e.KeyChar = " " Then
                RemoveHandler Form1.KeyPress, AddressOf Form1_KeyPress
                Play()
            End If
        Else

            RemoveHandler Form1.KeyPress, AddressOf Form1_KeyPress
            Endgame()
            AllMenus.RunMainMenu()
        End If

    End Sub

End Class
