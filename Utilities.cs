using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace SHETest
{
    public static class Utilities
    {

        static List<TableDatacollection> tableDatacollections = new List<TableDatacollection>();

        //Get the data from a html table and store it in a list
        public static void ReadTable(IWebElement table)
        {
            //Get All Columns from the table
            var columns = table.FindElements(By.TagName("th"));
            //Get All Rows from the table
            var rows = table.FindElements(By.XPath("//tbody/tr"));

            //Create rowIndex
            int rowIndex = 0;
            foreach (var row in rows)
            {
               //Create column index
                int columnIndex = 0;

                var columnData = row.FindElements(By.TagName("td"));

                //Add column data into list
                foreach (var columnValue in columnData)
                {
                   tableDatacollections.Add(new TableDatacollection
                    {
                        RowNumber = rowIndex,
                        ColumnName = columns[columnIndex].Text,
                        ColumnValue = columnValue.Text
                    });

                    //Move to Next column
                    columnIndex++;
                }

                    //Move to next row
                    rowIndex++;               
            }
        }

        // To get a cell value from the html table by passing its column name and row number
        public static String ReadCell(String columnName, int rowNumber)
        {
            var data = (from e in tableDatacollections
                        where e.ColumnName == columnName && e.RowNumber == rowNumber
                        select e.ColumnValue).SingleOrDefault();
            return data;
        }

        //To get a substring from a string that is present between 2 strings
        public static String GetSubStringBetweenStrings(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }

    public class TableDatacollection
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }

  }



