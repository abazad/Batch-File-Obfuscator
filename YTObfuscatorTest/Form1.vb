Imports System.Text

Public Class Form1

    Dim obfuscatedCode As New StringBuilder
    Dim alphaChars As String = "abcdefghijklmnopqrstuvwxyz"
    Dim randomGenerator As New Random

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            ObfuscateMethod1()
        ElseIf RadioButton2.Checked = True Then
            ObfuscateMethod2()
        ElseIf RadioButton3.Checked = True Then
            ObfuscateMethod3()
        End If
    End Sub

    Private Sub ObfuscateMethod1()
        TextBox1.ReadOnly = True

        obfuscatedCode.Clear() ' Make sure there is no excess code left over from last time.

        '
        ' GENERATE THE VARIABLE NAMES FOR EACH LOWERCASE ALPHA CHARACTER
        '
        obfuscatedCode.Append("@echo off" & vbNewLine) ' Prepend @echo off so that the variable definitions don't show on screen.
        Dim alphaCharDefinitions(26) As String ' Create a string array that will hold the variable name for each letter of the alphabet a-z.
        Dim loopCounter As Integer
        For loopCounter = 0 To 25 ' Loop 26 times for each character a-z.
            alphaCharDefinitions(loopCounter) = GenerateRandomString(6) ' Assign each character a randomly generated variable name.
            obfuscatedCode.Append("set " & alphaCharDefinitions(loopCounter) & "=" & Mid(alphaChars, loopCounter + 1, 1) & vbNewLine) ' Add the "set asdfjkl=a" into the StringBuilder
        Next

        '
        ' OBFUSCATE THE SOURCE CODE
        '
        For loopCounter = 1 To Len(TextBox1.Text) ' For each character in TextBox1
            Select Case Mid(TextBox1.Text, loopCounter, 1)
                Case "a"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(0) & "%") ' Replace each lowercase alpha character with its variable equivalent.
                Case "b"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(1) & "%")
                Case "c"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(2) & "%")
                Case "d"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(3) & "%")
                Case "e"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(4) & "%")
                Case "f"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(5) & "%")
                Case "g"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(6) & "%")
                Case "h"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(7) & "%")
                Case "i"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(8) & "%")
                Case "j"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(9) & "%")
                Case "k"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(10) & "%")
                Case "l"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(11) & "%")
                Case "m"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(12) & "%")
                Case "n"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(13) & "%")
                Case "o"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(14) & "%")
                Case "p"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(15) & "%")
                Case "q"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(16) & "%")
                Case "r"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(17) & "%")
                Case "s"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(18) & "%")
                Case "t"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(19) & "%")
                Case "u"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(20) & "%")
                Case "v"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(21) & "%")
                Case "w"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(22) & "%")
                Case "x"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(23) & "%")
                Case "y"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(24) & "%")
                Case "z"
                    obfuscatedCode.Append("%" & alphaCharDefinitions(25) & "%")
                Case Else
                    obfuscatedCode.Append(Mid(TextBox1.Text, loopCounter, 1)) ' If it does not match one of the alpha characters, just leave it alone and place it in.S
            End Select
        Next

        TextBox1.ReadOnly = False
        SaveFile()
    End Sub

    Private Sub ObfuscateMethod2()
        TextBox1.ReadOnly = True

        obfuscatedCode.Clear() ' Make sure there is no excess code left over from last time.
        Dim randomVariableName As String = ""

        '
        ' OBFUSCATE THE SOURCE CODE
        '
        For loopCounter = 1 To Len(TextBox1.Text) ' For each line in the source code, stick an uninitialized variable in between that expands to ""
            randomVariableName = GenerateRandomString(6)
            obfuscatedCode.Append("%" & randomVariableName & "%" & Mid(TextBox1.Text, loopCounter, 1))
        Next
        TextBox1.ReadOnly = False
        SaveFile()
    End Sub

    Private Sub ObfuscateMethod3()
        TextBox1.ReadOnly = True

        obfuscatedCode.Clear() ' Make sure there is no excess code left over from last time.

        Dim unicodeHeader() As Byte = New Byte() {&HFF, &HFE, &H0D, &H0A} ' The bytes that make text editors think it's in Unicode
        Dim obfuscatedCodeByteArray() As Byte = New Byte() {} ' Declare new Byte array for our text to be obfuscated.
        obfuscatedCodeByteArray = System.Text.Encoding.ASCII.GetBytes("cls" + vbNewLine + TextBox1.Text) ' Put TextBox1.Text into byte array
        Dim concatenatedByteArray() As Byte = New Byte(unicodeHeader.Length + obfuscatedCodeByteArray.Length) {} ' Make new array to combine header and code

        unicodeHeader.CopyTo(concatenatedByteArray, 0) ' Copy header to full byte array
        obfuscatedCodeByteArray.CopyTo(concatenatedByteArray, unicodeHeader.Length) ' Copy obfuscated code after the header

        TextBox1.ReadOnly = False
        ' Need a custom save function because have to write raw bytes to disk.
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            IO.File.WriteAllBytes(SaveFileDialog1.FileName, concatenatedByteArray) ' Write raw bytes, not just text, to the disk.
        Else
            Return
        End If
    End Sub

    Private Function SaveFile() As Boolean ' Returns true if DialogResult OK, false otherwise
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            IO.File.WriteAllText(SaveFileDialog1.FileName, obfuscatedCode.ToString)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    End Sub

    Private Function GenerateRandomString(length As Integer) As String
        Randomize()
        Dim i As Integer ' The Counter for the loop to generate random characters.
        Dim temp As String = "" ' A string to hold our randomly generated characters as they're created.
        For x As Integer = 0 To length
            i = CInt(Math.Floor((alphaChars.Length - 1 + 1) * Rnd())) + 1
            temp &= Mid(alphaChars, i, 1) ' Get a character from the alphabet at the position specified by the random number.
        Next
        Return temp
    End Function
End Class
