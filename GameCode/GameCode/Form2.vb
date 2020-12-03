Public Class Form2
    Dim boost As Integer = 10
    Dim r As New Random
    Dim score As Integer
    Sub randmove(P As PictureBox)
        Dim x As Integer
        Dim y As Integer
        x = r.Next(-10, 11)
        y = r.Next(-10, 11)
        MoveTo(P, x, y)
    End Sub


    Private Sub Form2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                Mike.Image.RotateFlip(RotateFlipType.Rotate270FlipY)
                Me.Refresh()
            Case Keys.Up
                MoveTo(Mike, 0, -boost)

            Case Keys.Down
                MoveTo(Mike, 0, boost)
            Case Keys.Left
                MoveTo(Mike, -boost, 0)
            Case Keys.Right
                MoveTo(Mike, boost, 0)

            Case Keys.Space
                Bullet.Location = Mike.Location
                Bullet.Visible = True
                Timer2.Enabled = True
            Case Keys.W
                move(Bullet, 0, -3)
            Case Keys.S
                move(Bullet, 0, 3)
            Case Keys.A
                move(Bullet, -3, 0)
            Case Keys.D
                move(Bullet, 3, 0)
        End Select
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        chase(Ghost1)
        randmove(Ghost1)
        chase(Ghost2)
        randmove(Ghost2)
        chase(Ghost3)
        randmove(Ghost3)
        '  chase(Ghost4)
        '   randmove(Ghost4)
        'Move(PictureBox1, 2, 5)
        'Move(PictureBox2, -2, 5)
        'Move(PictureBox3, 0, 5)
    End Sub
    Sub move(p As PictureBox, x As Integer, y As Integer)
        p.Location = New Point(p.Location.X + x, p.Location.Y + y)


    End Sub

    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(Mike.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub

    Public Sub chase(p As PictureBox)
        Dim x, y As Integer

        If p.Location.X > Mike.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < Mike.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
        'Ghost 
        If p.Location.X > Ghost1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        If p.Location.Y < Ghost1.Location.Y Then
            y = 5
        Else
            y = -5

        End If
    End Sub

    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function

    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If

        '
        Dim other As Object = Nothing
        If p.Name = "Mike" And Collision(p, "PictureBox14") Then
            Win.Visible = True
            Dim f As New Form2
            f.ShowDialog()
            Me.Visible = True
            Return
        End If

        If p.Name = "Mike" And Collision(p, "Paper", other) Then

            other.Visible = False
            boost = boost + 1
            score = score + 1

        End If
        If p.Name.Contains("Ghost") And Collision(p, "Bullet") Then
            p.Visible = False
        End If
        If p.Name = "Mike" And Collision(p, "Ghost") Then
            p.Visible = False


        End If
        If p.Name = "Mike" And Collision(p, "Ghost") Then
            'Lose.Visible = True

        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ScoreLabel1.Text = score
    End Sub

    Private Sub Timer3_Tick()

    End Sub

    Private Sub ScoreLabel1_Click(sender As Object, e As EventArgs) Handles ScoreLabel1.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class