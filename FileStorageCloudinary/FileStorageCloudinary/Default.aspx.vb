
Imports System.Data
Imports System.Drawing
Imports System.IO


Partial Class _Default
    Inherits Page
    Sub Page_Load(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Do stuff
        'Get All Image Urls
        Dim imageFilePath As String = Path.Combine(Server.MapPath("/Content"), "images.txt")
        Dim ImagefileUrlList = ReadUrls(imageFilePath)
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow
        dt.Columns.Add("Image", GetType(String))
        For Each a In ImagefileUrlList
            dr = dt.NewRow()
            dr.SetField(Of String)("Image", a)
            dt.Rows.Add(dr)
        Next

        GridView1.DataSource = dt
        GridView1.DataBind()


        'Get All Video Urls
        Dim videoFilePath As String = Path.Combine(Server.MapPath("/Content"), "videos.txt")
        Dim videofileUrlList = ReadUrls(videoFilePath)
        Dim dtable As DataTable = New DataTable()
        Dim drow As DataRow
        dtable.Columns.Add("Video", GetType(String))
        For Each v In videofileUrlList
            drow = dtable.NewRow()
            drow.SetField(Of String)("Video", v)
            dtable.Rows.Add(drow)
        Next
        GridView2.DataSource = dtable
        GridView2.DataBind()
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If fileUploadControl.HasFile Then
            Try
                If fileUploadControl.PostedFile.ContentType.Contains("video") OrElse fileUploadControl.PostedFile.ContentType.Contains("image") Then
                    Dim filename As String = Path.GetFileName(fileUploadControl.FileName)
                    Dim localPath As String = Path.Combine(HttpContext.Current.Server.MapPath("/Content"), filename)
                    fileUploadControl.SaveAs(Path.Combine(localPath))
                    Dim account As CloudinaryDotNet.Account = New CloudinaryDotNet.Account("dgvvxs3ek", "151391995718976", "SuNoUzYHGyeP9-IY_DGsLJfk2G0")
                    Dim cloudinary As CloudinaryDotNet.Cloudinary = New CloudinaryDotNet.Cloudinary(account)
                    cloudinary.Api.Timeout = 1200000

                    'Image Upload
                    If fileUploadControl.PostedFile.ContentType.Contains("image") Then
                        Dim uploadParams = New CloudinaryDotNet.Actions.ImageUploadParams
                        uploadParams.File = New CloudinaryDotNet.FileDescription(localPath)
                        uploadParams.Tags = "new,image"
                        Dim uploadResults = cloudinary.Upload(uploadParams)
                        Dim Url As String = uploadResults.SecureUri.ToString()
                        Dim ImagesPath As String = Path.Combine(Server.MapPath("/Content"), "images.txt")
                        LogWrite(Url, ImagesPath)
                        fileUploadStatus.Text = "File uploaded Success..! "
                        fileUploadStatus.ForeColor = Color.Green
                    End If


                    'Video Upload
                    If fileUploadControl.PostedFile.ContentType.Contains("video") Then
                        Dim uploadParams = New CloudinaryDotNet.Actions.VideoUploadParams()
                        uploadParams.Tags = "video,new"
                        uploadParams.File = New CloudinaryDotNet.FileDescription(localPath)
                        uploadParams.Transformation = New CloudinaryDotNet.Transformation().Width(400).Height(260).Crop("limit")
                        Dim uploadResult = cloudinary.UploadLarge(uploadParams)
                        Dim Url As String = uploadResult.SecureUri.ToString()
                        Dim VideosPath As String = Path.Combine(Server.MapPath("/Content"), "videos.txt")
                        LogWrite(Url, VideosPath)
                        fileUploadStatus.Text = "File uploaded Success..! "
                        fileUploadStatus.ForeColor = Color.Green
                    End If
                    If File.Exists(localPath) Then
                        File.Delete(localPath)
                    End If
                Else
                    fileUploadStatus.Text = "Only Image & Video files are accepted..!"
                    fileUploadStatus.ForeColor = Color.Red
                End If
            Catch ex As Exception
                fileUploadStatus.Text = "File Upload Error : " & ex.Message
                fileUploadStatus.ForeColor = Color.Red
            End Try
        End If
    End Sub
End Class