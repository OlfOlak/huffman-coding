﻿using Microsoft.Win32;
using System;
using System.Windows;
using ConsoleAsmTestTypes;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static DllCSharp.CharFrequency;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static String libSelected;
        private static String filepath;

        private const String COMPRESSED_FILENAME = "compression.txt";
        private const String DECOMPRESSED_FILENAME = "decompressed.txt";
        private const String INPUT_FILE_FILTER = "Text files (*.txt)|*.txt|All file (*.*)|*.*";
        private const String OUTPUT_FILE_FILTER = "Text file(*.txt)|*.txt";


        public static int[] flag = { 1, 1, 1, 1 };
        public static int[] resultData = new int[255];

        [DllImport("DllAsm.dll")]
        private static unsafe extern uint CountCharFrequencyAsm(int* a, long* b, int* c);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = INPUT_FILE_FILTER;
            if (openFileDialog.ShowDialog() == true)
            {
                String filename = openFileDialog.FileName;
                filenameText.Text = filename.Substring(filename.LastIndexOf('\\') + 1);
                filepath = openFileDialog.FileName;
                FileInfo fileinfo = new FileInfo(filepath);
                filesizeText.Text = getFileSize(fileinfo.Length);

                validateFileInput();
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            validateRadioButtonsCheck();
            validateFileInput();

            
            if (libSelected != null && filepath != null)
            {
                run(libSelected, filepath);
            }
        }

        private String getFileSize(long length)
        {
            return Convert.ToString(length) + " B";
            //if (length < 1024)
            //{
            //    return Convert.ToString(length) + " B";
            //} else
            //{
            //    return Convert.ToString(length / 1024) + " kB";
            //}
        }

        private void validateRadioButtonsCheck()
        {
            if (csharpRadioButton.IsChecked == true || assemblyRadioButton.IsChecked == true)
            {
                radioButtonError.Visibility = Visibility.Hidden;
            } else
            {
                radioButtonError.Visibility = Visibility.Visible;
            }          
        }

        private void validateFileInput()
        {
            if (filepath == null)
            {
                fileError.Visibility = Visibility.Visible;
            } else
            {
                fileError.Visibility = Visibility.Hidden;
            }
        }

        private void csharpRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            validateRadioButtonsCheck();
            libSelected = "c#";
        }

        private void assemblyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            validateRadioButtonsCheck();
            libSelected = "asm";
        }

        private void btnSaveCompressedFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = OUTPUT_FILE_FILTER;
            saveFileDialog.FileName = COMPRESSED_FILENAME;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, File.ReadAllText(COMPRESSED_FILENAME));
            }
        }

        private void btnSaveDecompressedFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = OUTPUT_FILE_FILTER;
            saveFileDialog.FileName = DECOMPRESSED_FILENAME;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, File.ReadAllText(DECOMPRESSED_FILENAME));
            }
        }

        private void run(string libSelected, string filepath)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<HuffmanNode> nodeList;
            int[] readFile = Huffman.getIntsArrayFromFile(filepath);

            if (libSelected == "asm")
            {
                unsafe
                {
                    fixed (int* aArg1Addr = &readFile[0], aArg2Addr = &flag[0], aResultsAddr = &resultData[0])
                    {
                        var fileLength = Convert.ToInt64(readFile.Length);
                        CountCharFrequencyAsm(aArg1Addr, &fileLength, aResultsAddr);                
                    }
                }

                Huffman.charFrequency = resultData;
            } else {
                Huffman.charFrequency = CountCharFrequency(readFile, resultData);
            }

            nodeList = Huffman.getListFromFile(readFile);

            Huffman.getTreeFromList(nodeList);

            Huffman.setCodeToTheTree("", nodeList[0]);

            Huffman.saveCompressedTree(COMPRESSED_FILENAME);

            byte[] compressedFile = File.ReadAllBytes(COMPRESSED_FILENAME);

            Huffman.convertByteToBits(compressedFile);

            Huffman.saveDecompressedTree(nodeList[0], DECOMPRESSED_FILENAME);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            processingTimeText.Text = Convert.ToString(elapsedMs) + " ms";

            compressedStackPanel.Visibility = Visibility.Visible;
            decompressedStackPanel.Visibility = Visibility.Visible;
            processingTimeStackPanel.Visibility = Visibility.Visible;

            compressedFilenameText.Text = COMPRESSED_FILENAME;
            compressedFilesizeText.Text = (new FileInfo(COMPRESSED_FILENAME)).Length.ToString() + " B";
            decompressedFilenameText.Text = DECOMPRESSED_FILENAME;
            decompressedFilesizeText.Text = (new FileInfo(DECOMPRESSED_FILENAME)).Length.ToString() + " B";
        }
    }
}
