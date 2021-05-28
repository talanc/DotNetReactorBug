Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Log("Getting icon")

        Dim ico = IconHelper.GetFolderIcon()

        Log("Setting icon")

        TreeView1.ImageList = New ImageList()
        TreeView1.ImageList.Images.Add(ico)
        TreeView1.Nodes.Add(New TreeNode("Node", 0, 0))

        Log("End of function")
    End Sub

    Private Sub Log(msg As String)
        If CheckBox1.Checked Then
            MsgBox(msg)
        End If
    End Sub
End Class
