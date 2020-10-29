// (C) Copyright 2017-2021 by Andrew Nicholas. 
//
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//
// (C) Copyright 2011 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software
// in object code form for any purpose and without fee is hereby
// granted, provided that the above copyright notice appears in
// all copies and that both that copyright notice and the limited
// warranty and restricted rights notice below appear in all
// supporting documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK,
// INC. DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL
// BE UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is
// subject to restrictions set forth in FAR 52.227-19 (Commercial
// Computer Software - Restricted Rights) and DFAR 252.227-7013(c)
// (1)(ii)(Rights in Technical Data and Computer Software), as
// applicable.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ADNPlugin.Revit.FileUpgrader
{
  public partial class UpgraderForm : System.Windows.Forms.Form
  {
    ExternalCommandData cmdData;
    ArrayList fileTypes = new ArrayList();
    StreamWriter writer = null;
    IList<FileInfo> files = new List<FileInfo>();
    IList<String> failures = new List<String>();
    int success;
    int failed;

    // Variable to store if user cancels the process

    bool cancelled = false;
    bool addInfo = false;  
 
    // Container for previous opened document

    UIDocument previousDocument = null;

    public UpgraderForm( ExternalCommandData commandData )
    {
      InitializeComponent();

      // Keep a local copy of the command data

      cmdData = commandData;

      previousDocument = null; 
    }

    // Handler for Source folder browse button
    private void btnSource_Click( object sender, EventArgs e )       
    {
      // Open the folder browser dialog

      FolderBrowserDialog dlg = new FolderBrowserDialog();

      // Disable New Folder button since it is source location

      dlg.ShowNewFolderButton = false;

      // Provide description 

      dlg.Description = "Select the Source folder :";

      // Show the folder browse dialog

      dlg.ShowDialog();

      // Populate the source path text box

      txtSrcPath.Text = dlg.SelectedPath;
    }

    // Handler for the Destination folder browse button
    private void btnDestination_Click(object sender, EventArgs e)
    {
      // Open the folder browser dialog

      FolderBrowserDialog dlgDest = new FolderBrowserDialog();

      // Enable the New folder button since users should have
      // ability to create destination folder incase it did 
      // not pre-exist

      dlgDest.ShowNewFolderButton = true;

      // Provide description

      dlgDest.Description = "Select the Destination folder : ";

      // Show the folder browse dialog

      dlgDest.ShowDialog();

      // Populate the destination path text box

      txtDestPath.Text = dlgDest.SelectedPath;
    }

    // Handler for the Cancel button
    private void btnCancel_Click( object sender, EventArgs e )
    {
      // Set the cancelled variable to true

      cancelled = true;
      this.Close();
    }

    public void TraverseAll( DirectoryInfo source,
        DirectoryInfo target )
    {
      try
      {
        // Check for user input events

        System.Windows.Forms.Application.DoEvents();

        // If destination directory does not exist, 
        // create new directory

        if (!Directory.Exists(target.FullName))
        {
          Directory.CreateDirectory(target.FullName);
        }
         
        foreach (FileInfo fi in source.GetFiles())
        {
          // Check for user input events

          System.Windows.Forms.Application.DoEvents();
          if (!cancelled)
          {
            System.Security.AccessControl.FileSecurity sec =
              fi.GetAccessControl();
            if (!sec.AreAccessRulesProtected)
            {
              // Proceed only if it is not a back up file

              if (IsNotBackupFile(fi))
              {
                // Check if the file already exists, if not proceed
  
                if (!AlreadyExists(target, fi))
                {
                  // The method contains the code to upgrade the file

                  Upgrade(fi, target.FullName);
                }
                else
                {
                  // Print that the file already exists

                  String msg = " already exists!";
                  writer.WriteLine("------------------------------");
                  writer.WriteLine("Error: " 
                    + target.FullName + "\\" + fi.Name + " " + msg);
                  writer.WriteLine("------------------------------");
                  writer.Flush();

                  lstBxUpdates.Items.Add(
                    "-------------------------------");
                  lstBxUpdates.Items.Add("Error: " 
                    + target.FullName + "\\" + fi.Name + " " + msg);
                  lstBxUpdates.Items.Add(
                    "-------------------------------");
                  lstBxUpdates.TopIndex = lstBxUpdates.Items.Count-1; 
                }
              }
            }
            else
            {
              String msg = " is not accessible or read-only!";
              writer.WriteLine("-------------------------------");
              writer.WriteLine("Error: " + fi.FullName + msg);
              writer.WriteLine("-------------------------------");
              writer.Flush();
                
              lstBxUpdates.Items.Add(
                "------------------------------");
              lstBxUpdates.Items.Add("Error: " + fi.FullName + msg);
              lstBxUpdates.Items.Add(
                "------------------------------");
              lstBxUpdates.TopIndex = lstBxUpdates.Items.Count - 1; 
            }
          }
        }

        // Check for user input events

        System.Windows.Forms.Application.DoEvents();

        // RFT resave creates backup files 
        // Delete these backup files created
        foreach (FileInfo backupFile in target.GetFiles())
        {
          if (!IsNotBackupFile(backupFile))
            File.Delete(backupFile.FullName);
        }
          
        // Using recursion to work with sub-directories

        foreach (DirectoryInfo sourceSubDir in
          source.GetDirectories())
        {
          DirectoryInfo nextTargetSubDir =
            target.CreateSubdirectory(sourceSubDir.Name);
          TraverseAll(sourceSubDir, nextTargetSubDir);

          // Delete the empty folders - this is created when
          // none of the files in them meet our upgrade criteria

          if (nextTargetSubDir.GetFiles().Count() == 0 && 
            nextTargetSubDir.GetDirectories().Count() == 0)
            Directory.Delete(nextTargetSubDir.FullName);

          
        }
      }
      catch
      {
    
      }
    }

    // Helper method to check if file already exists in target folder
    private bool AlreadyExists(DirectoryInfo target, FileInfo file)
    {
      foreach (FileInfo infoTarget in target.GetFiles())
      {
        if (infoTarget.Name.Equals(file.Name))
        return true;
      }        
      return false;
    }

    // Helps determine if the source file is back up file or not
    // Backup files are determined by the format : 
    // <project_name>.<nnnn>.rvt
    // This utility ignores backup files
    private bool IsNotBackupFile(FileInfo rootFile)
    {       
      // Check if the file is a backup file

      if (rootFile.Name.Length < 9)
      {
        return true;
      }
      else
      {
        if (rootFile.Name.Substring(rootFile.Name.Length - 9)
          .Length > 0)
        {
          String backUpFileName = rootFile.Name.Substring(
            rootFile.Name.Length - 9);
          long result = 0;

          // Check each char in the file name if it follows 
          // the back up file naming convention

          if (
            backUpFileName[0].ToString().Equals(".")
              && Int64.TryParse(backUpFileName[1].ToString(), out result)
              && Int64.TryParse(backUpFileName[2].ToString(), out result)
              && Int64.TryParse(backUpFileName[3].ToString(), out result)
              && Int64.TryParse(backUpFileName[4].ToString(), out result)
              )
          return false;
        }
        return true;
      }
    }

    // Searches the directory and creates an internal list of files
    // to be upgraded
    void SearchDir( DirectoryInfo sDir, bool first )
    {
      try
      {
        // If at root level, true for first call to this method

        if( first )
        {
          foreach( FileInfo rootFile in sDir.GetFiles() )
          {
            // Create internal list of files to be upgraded
            // This will help with Progress bar
              
            // Proceed only if it is not a back up file

            if (IsNotBackupFile(rootFile))
            {
              // Keep adding files to the internal list of files

              if (fileTypes.Contains(rootFile.Extension)
                || rootFile.Extension.Equals(".txt"))
              {
                if (rootFile.Extension.Equals(".txt"))
                {
                  if (fileTypes.Contains(".rfa"))
                  {
                    foreach (FileInfo rft in sDir.GetFiles())
                    {
                      if (
                        rft.Name.Remove(rft.Name.Length - 4, 4)
                        .Equals(
                        rootFile.Name.Remove(
                        rootFile.Name.Length - 4, 4)
                        ) && 
                        !(rft.Extension.Equals(rootFile.Extension))
                        )
                      { files.Add(rootFile); break; }
                    }
                  }
                }
                else
                  files.Add(rootFile);
              }
            }
         }
      }
            
        // Get access to each sub-directory in the root directory

        foreach (DirectoryInfo direct in sDir.GetDirectories())
        {
          System.Security.AccessControl.DirectorySecurity sec =
            direct.GetAccessControl();
          if (!sec.AreAccessRulesProtected)
          {
            foreach (FileInfo fInfo in direct.GetFiles())
            {
              // Proceed only if it is not a back up file

              if (IsNotBackupFile(fInfo))
              {                
                // Keep adding files to the internal list of files

                if (fileTypes.Contains(fInfo.Extension)
                  || fInfo.Extension.Equals(".txt"))
                {
                  if (fInfo.Extension.Equals(".txt"))
                  {
                    if (fileTypes.Contains(".rfa"))
                    {
                      foreach (FileInfo rft in direct.GetFiles())
                      {
                        if (
                          rft.Name.Remove(
                          rft.Name.Length - 4, 4).Equals(
                          fInfo.Name.Remove(fInfo.Name.Length - 4, 4)
                          )
                          && !(rft.Extension.Equals(fInfo.Extension))
                          )
                        { files.Add(fInfo); break; }
                      }                              
                    }
                  }
                  else
                    files.Add(fInfo);
                }               
              }
            }

            // Use recursion to drill down further into 
            // directory structure

            SearchDir(direct, false);
          }
          else
          {
            String msg = " is not accessible or read-only!";
            writer.WriteLine("------------------------------------");
            writer.WriteLine("Error: " + direct.FullName + msg);
            writer.WriteLine("------------------------------------");
            writer.Flush();

            lstBxUpdates.Items.Add("------------------------------");
            lstBxUpdates.Items.Add("Error: "+ direct.FullName + msg);
            lstBxUpdates.Items.Add("------------------------------");
            lstBxUpdates.TopIndex = lstBxUpdates.Items.Count - 1; 
          }
        }
      }
      catch( Exception excpt )
      {
        writer.WriteLine("-------------------------------------");
        writer.WriteLine("Error :" + excpt.Message);
        writer.WriteLine("-------------------------------------");
        writer.Flush();
      }
    }

    // Handler code for the Upgrade button click event
    private void btnUpgrade_Click( object sender, EventArgs e )
    {
      // Initialize the count for success and failed files

      success = 0;
      failed = 0;
      fileTypes.Clear();

      // Create a list of Revit file types based on 
      // types selected in dialog

      if( chkBoxRFA.Checked ) { fileTypes.Add( ".rfa" ); }
      if( chkBoxRVT.Checked ) { fileTypes.Add( ".rvt" ); }
      if( chkBoxRTE.Checked ) { fileTypes.Add( ".rte" ); }
      if( chkBoxRFT.Checked ) { fileTypes.Add(".rft"); }

      // Error handling with file types

      if( fileTypes.Count == 0 )
      {
        TaskDialog.Show( "No File Types",
          "Please select at least one file type!" );
        return;
      }

      // Ensure all path information is filled in 

      if (txtSrcPath.Text.Length > 0
        && txtDestPath.Text.Length > 0)
      {

        // Perform checks to see if all the paths are valid

        DirectoryInfo dir = new DirectoryInfo(txtSrcPath.Text);
        DirectoryInfo dirDest = new DirectoryInfo(txtDestPath.Text);

        if (!dir.Exists)
        {
          txtSrcPath.Text = String.Empty;
          return;
        }

        if (!dirDest.Exists)
        {
          txtDestPath.Text = String.Empty;
          return;
        }

        // Ensure destination folder is not inside the source folder
        var dirs = from nestedDirs in dir.EnumerateDirectories("*")
                   where dirDest.FullName.Contains(nestedDirs.FullName)
                   select nestedDirs;
        if (dirs.Count() > 0)
        {
          TaskDialog.Show(
            "Abort Upgrade",
            "Selected Destination folder, " + dirDest.Name +
            ", is contained in the Source folder. Please select a" +
            " Destination folder outside the Source folder.");
          txtDestPath.Text = String.Empty;
          return;
        }

        // If paths are valid
        // Create log and initialize it

        writer = File.CreateText(
          txtDestPath.Text + "\\" + "UpgraderLog.txt"
          );

        // Clear list box 

        lstBxUpdates.Items.Clear();
        files.Clear();

        // Progress bar initialization

        bar.Minimum = 1;

        // Search the directory and create the 
        // list of files to be upgraded

        SearchDir(dir, true);

        // Set Progress bar base values for progression

        bar.Maximum = files.Count;
        bar.Value = 1;
        bar.Step = 1;

        // Traverse through source directory and upgrade
        // files which match the type criteria
          
        TraverseAll(
          new System.IO.DirectoryInfo(txtSrcPath.Text),
          new System.IO.DirectoryInfo(txtDestPath.Text));

        // In case no files were found to match 
        // the required criteria

        if (failed.Equals(0) && success.Equals(0))
        {
          String msg = "No relevant files found for upgrade!";
          TaskDialog.Show("Incomplete", msg);
          writer.WriteLine(msg);
          writer.Flush();
        }
        else
        {
          if (failures.Count > 0)
          {
            String msg = "-------------"
              + "List of files that "
              + "failed to be upgraded"
              + "--------------------";

            // Log failed files information

            writer.WriteLine("\n");
            writer.WriteLine(msg);
            writer.WriteLine("\n");
            writer.Flush();

            // Display the failed files information

            lstBxUpdates.Items.Add("\n");
            lstBxUpdates.Items.Add(msg);
            lstBxUpdates.Items.Add("\n");
            lstBxUpdates.TopIndex = lstBxUpdates.Items.Count - 1; 
            foreach (String str in failures)
            {
              writer.WriteLine(str);
              lstBxUpdates.Items.Add("\n" + str);
              lstBxUpdates.TopIndex = lstBxUpdates.Items.Count - 1; 
            }
            failures.Clear();
            writer.Flush();
          }

          // Display final completion dialog 
          // with success rate

          TaskDialog.Show("Completed",
            success + "/" + (success + failed)
            + " files have been successfully upgraded! "
            + "\n\nA log file has been created at :\n"
            + txtDestPath.Text);
        }
        // Reset the Progress bar

        bar.Value = 1;
          
        // Close the Writer object

        writer.Close();
      }
    }

    // Method which upgrades each file
    private void Upgrade( FileInfo file, String destPath )
    {
      addInfo = false;

      // Check if file type is what is expected to be upgraded
      // or is a text file which is for files which contain 
      // type information for certain family files

      if( fileTypes.Contains( file.Extension )
        || file.Extension.Equals( ".txt" ) )
      {
        try
        {
          // If it is a text file

          if( file.Extension.Equals( ".txt" ) )
          {
            if( fileTypes.Contains( ".rfa" ) )
            {
              bool copy = false;

              // Check each file from the list to see 
              // if the text file has the same name as 
              // any of the family files or if it is 
              // just a standalone text file. In case 
              // of standalone text file, ignore.

              foreach( FileInfo rft in files )
              {
                if( 
                  rft.Name.Remove(rft.Name.Length - 4, 4).Equals(
                  file.Name.Remove(file.Name.Length - 4, 4))
                  && !(rft.Extension.Equals(file.Extension)) 
                  )
                { copy = true; break; }
              }
              if( copy )
              {

                // Copy the text file into target 
                // destination

                File.Copy( file.DirectoryName +
                  "\\" + file.Name, destPath +
                  "\\" + file.Name, true );
                addInfo = true;
              }
            }
          }

          // For other file types other than text file

          else
          {
            // This is the main function that opens and save  
            // a given file. 

            // Check if the file is RFT file
            // Since we have to use OpenDocumentFile for RFT files
            if (file.Extension.Equals(".rft"))
            {

              Document doc =
                cmdData.Application.Application.OpenDocumentFile(
                file.FullName);

              String destinationFile = destPath + "\\" + file.Name;

              Transaction trans = new Transaction(doc, "T1");
              trans.Start();
              doc.Regenerate();
              trans.Commit();

              doc.SaveAs(destinationFile);
              doc.Close();
    
              addInfo = true;
            }
            else
            {

              // Open a Revit file as an active document. 

              UIApplication UIApp = cmdData.Application;
              UIDocument UIDoc = UIApp.OpenAndActivateDocument(file.FullName);

              Document doc = UIDoc.Document; 

              // Try closing the previously opened document after 
              // another one is opened. We are doing this because we 
              // cannot explicitely close an active document
              //  at a moment.  

              if (previousDocument != null)
              {
                previousDocument.SaveAndClose();
              }

              // Save the Revit file to the target destination.
              // Since we are opening a file as an active document, 
              // it takes care of preview. 

              String destinationFile = destPath + "\\" + file.Name;
              doc.SaveAs(destinationFile);

              // Saving the current document to close it later.   
              // If we had a method to close an active document, 
              // we want to close it here. However, since we opened 
              // it as an active document, we cannot do so.
              // We'll close it after the next file is opened.

              previousDocument = UIDoc; 

              // Set variable to know if upgrade 
              // was successful - for status updates

              addInfo = true;
            }
          }

          if (addInfo)
          {
            String msg = " has been upgraded";

            // Log file and user interface updates

            lstBxUpdates.Items.Add( "\n" + file.Name + msg );
            lstBxUpdates.TopIndex = lstBxUpdates.Items.Count - 1; 
            writer.WriteLine( file.FullName + msg );
            writer.Flush();
            bar.PerformStep();
            ++success;
          }
        }
        catch( Exception ex )
        {
          failures.Add( file.FullName 
            + " could not be upgraded: "
            + ex.Message );

          bar.PerformStep();

          ++failed;
        }
      }
    }
  }
}