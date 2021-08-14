using CameraSearch.Models;
using CsvHelper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CameraSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        [HttpGet]
        [Route("ConvertCSVtoDataTable111")]
        public DataTable ConvertCSVtoDataTable()
        {
            string strFilePath = "d://cameras-defb.csv";
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                //string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                string[] rows = sr.ReadLine().Split(";");
                DataRow dr = dt.NewRow();
                try
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }

        [HttpGet]
        [Route("ConvertCSVtoDataTable")]
        public List<UserDetails> read()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvUserDetailsMapping csvMapper = new CsvUserDetailsMapping();
            CsvParser<UserDetails> csvParser = new CsvParser<UserDetails>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromFile(@"d://cameras-defb.csv", Encoding.ASCII)
                         .ToList();
            Console.WriteLine("Name " + "ID   " + "City  " + "Country");
            List<UserDetails> cameraDetails = new List<UserDetails>();
            
            int counter = 1;
            foreach (var details in result)
            {
                try
                {
                    string[] Columns = details.Result.ID.Split(";");
                    UserDetails camera = new UserDetails();
                    camera.Index = counter;
                    camera.Name = Columns[0];
                    camera.Latitude = Columns[1];
                    camera.Longitude = Columns[2];
                    cameraDetails.Add(camera);
                    counter += 1;
                }
                catch (Exception ex)
                {


                }

           

                //Console.WriteLine(details.Result.Name + " " + details.Result.ID + " " + details.Result.City + " " + details.Result.Country);
            }

            return cameraDetails;

            //         var directory = result
            //.SelectMany(p => p.Result,
            //            (parent, child) => new { parent.Result, child.ToString() });
            //         return result;

        }


    }



}



