using IndianStateCensusAnalyzerProgram;
using IndianStateCensusAnalyzerProgram.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace IndianStateCensusAnalyzerTesting
{
    [TestClass]
    public class IndianStateAnalyzerTestCases
    {
        string stateCensusFilePath = @"C:\Users\Guruprasad\source\repos\IndianStateCensusAnalyzerProgram\IndianStateCensusAnalyzerProgram\CSVFiles\IndianPopulation.csv";
        string wrongFilePath = @"C:\Users\Guruprasad\source\repos\IndianStateCensusAnalyzerProgram\IndianStateCensusAnalyzerProgram\CSVFiles\WrongIndianPopulation.csv";
        string wrongTypeFilePath = @"C:\Users\Guruprasad\source\repos\IndianStateCensusAnalyzerProgram\IndianStateCensusAnalyzerProgram\CSVFiles\IndianPopulation.txt";
        string wrongDelimiterFilePath = @"C:\Users\Guruprasad\source\repos\IndianStateCensusAnalyzerProgram\IndianStateCensusAnalyzerProgram\CSVFiles\DelimiterIndiaStateCensusData.csv";
        string wrongHeaderFilePath = @"C:\Users\Guruprasad\source\repos\IndianStateCensusAnalyzerProgram\IndianStateCensusAnalyzerProgram\CSVFiles\WrongIndiaStateCensusData.csv";

        CSVAdapterFactory csvAdapter;
        Dictionary<string, CensusDTO> stateRecords;

        [TestInitialize]
        public void SetUp()
        {
            csvAdapter = new CSVAdapterFactory();
            stateRecords = new Dictionary<string, CensusDTO>();
        }

        //TC 1.1 Given correct path To ensure number of records matches
        [TestMethod]
        public void GivenStateCensusCsvReturnStateRecords()
        {
            int expected = 29;
            stateRecords = csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, stateCensusFilePath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(expected, stateRecords.Count);
        }

        //TC 1.2 Given incorrect file should return custom exception - File not found
        [TestMethod]
        public void GivenWrongFileReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.FILE_NOT_FOUND;
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.3 Given correct path but incorrect file type should return custom exception - Invalid file type
        [TestMethod]
        public void GivenWrongFileTypeReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE;
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.4 Given correct path but wrong delimiter should return custom exception - File Containers Wrong Delimiter
        [TestMethod]
        public void GivenWrongDelimiterReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER;
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongDelimiterFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.5 Given correct path but wrong header should return custom exception - Incorrect header in Data
        [TestMethod]
        public void GivenWrongHeaderReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INCORRECT_HEADER;
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }
    }
}
