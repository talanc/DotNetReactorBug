Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices

Public Class IconHelper
    Public Shared Function SafeExtractAssociatedIcon(filePath As String) As Icon
        Try
            Return Icon.ExtractAssociatedIcon(filePath)
        Catch
            Dim tempFilePath = Path.Combine(Path.GetTempPath(), "CheckpointIcon" & Path.GetExtension(filePath))
            Try
                File.Copy(filePath, tempFilePath, True)
                Return Icon.ExtractAssociatedIcon(tempFilePath)
            Catch
                Return Nothing
            Finally
                Try
                    File.Delete(tempFilePath)
                Catch
                End Try
            End Try
        End Try
    End Function

    ' https://web.archive.org/web/20130814062331/http://www.codeguru.com/csharp/csharp/cs_misc/icons/article.php/c4261/Getting-Associated-Icons-Using-C.htm
    Private Class Win32
        Public Const SHGFI_ICON As UInteger = &H100
        Public Const SHGFI_LARGEICON As UInteger = &H0
        Public Const SHGFI_SMALLICON As UInteger = &H1

        <StructLayout(LayoutKind.Sequential)>
        Public Structure SHFILEINFO
            Public hIcon As IntPtr
            Public iIcon As IntPtr
            Public dwAttributes As UInteger
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
            Public szDisplayName As String
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
            Public szTypeName As String
        End Structure

        Public Declare Auto Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, ByVal dwFileAttributes As UInteger, ByRef psfi As SHFILEINFO, ByVal cbSizeFileInfo As UInteger, ByVal uFlags As UInteger) As IntPtr
        Public Declare Auto Function DestroyIcon Lib "user32.dll" (ByVal hIcon As IntPtr) As Integer
    End Class

    Public Shared Function GetFolderIcon() As Icon
        ' Fix / workaround for .NET Reactor
        ' Program is crashing when protected
        ' So return Nothing until we find a proper fix (whatever that is...)
        'Return Nothing

        Dim systemDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.System)

        Dim shinfo As New Win32.SHFILEINFO()

        Try
            Dim smallIconPtr = Win32.SHGetFileInfo(systemDir, 0, shinfo, Convert.ToUInt32(Marshal.SizeOf(shinfo)), Win32.SHGFI_ICON Or Win32.SHGFI_SMALLICON)

            Dim ico = TryCast(Icon.FromHandle(shinfo.hIcon).Clone(), Icon)

            Return ico
        Catch ex As Exception
            Debug.Fail(ex.ToString())
        Finally
            If shinfo.hIcon <> IntPtr.Zero Then
                Try
                    Win32.DestroyIcon(shinfo.hIcon)
                Catch ex As Exception
                    Debug.Fail(ex.ToString())
                End Try
            End If
        End Try

        Return Nothing
    End Function
End Class
