using System;
using Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
	[TestClass]
	public class RowParserTests
	{
		[TestMethod]
		public void EmptyRowTest()
		{
			var input = "";
			var parser = new RowParser();

			parser.Parse(input);

			Assert.AreEqual(0, parser.Count);
			Assert.AreEqual(false, parser.HasNumber);
		}

		[TestMethod]
		public void OneColumn()
		{
			var input = RowMaker("Test");
			var parser = new RowParser();

			var expectedColumnName = "Test";
			parser.Parse(input);

			Assert.AreEqual(1, parser.Count);
			Assert.AreEqual(expectedColumnName, parser.Slices[0]);
			Assert.AreEqual(false, parser.HasNumber);
		}

		[TestMethod]
		public void OneColumnIn2Line()
		{
			var input = LineMaker("Test", "Test2");
			var parser = new RowParser();

			var expectedColumnName = "Test";
			parser.Parse(input);

			Assert.AreEqual(1, parser.Count);
			Assert.AreEqual(expectedColumnName, parser.Slices[0]);
			Assert.AreEqual(false, parser.HasNumber);
		}

		[TestMethod]
		public void NumberColumn()
		{
			var input = RowMaker("1235");
			var parser = new RowParser();

			var expectedColumnName = "1235";
			parser.Parse(input);

			Assert.AreEqual(1, parser.Count);
			Assert.AreEqual(expectedColumnName, parser.Slices[0]);
			Assert.AreEqual(true, parser.HasNumber);
		}

		[TestMethod]
		public void NumberColumnIn2Line()
		{
			var input = LineMaker("1235", "Test2");
			var parser = new RowParser();

			var expectedColumnName = "1235";
			parser.Parse(input);

			Assert.AreEqual(1, parser.Count);
			Assert.AreEqual(expectedColumnName, parser.Slices[0]);
			Assert.AreEqual(true, parser.HasNumber);
		}

		[TestMethod]
		public void TwoColumn()
		{
			var input = RowMaker("One", "Two");
			var parser = new RowParser();

			var expectedColumn1Name = "One";
			var expectedColumn2Name = "Two";
			parser.Parse(input);

			Assert.AreEqual(2, parser.Count);
			Assert.AreEqual(expectedColumn1Name, parser.Slices[0]);
			Assert.AreEqual(expectedColumn2Name, parser.Slices[1]);
			Assert.AreEqual(false, parser.HasNumber);
		}

		[TestMethod]
		public void TwoColumnWithNumber()
		{
			var input = RowMaker("One", "Two", "123");
			var parser = new RowParser();

			var expectedColumn1Name = "One";
			var expectedColumn2Name = "Two";
			var expectedColumn3Name = "123";
			parser.Parse(input);

			Assert.AreEqual(3, parser.Count);
			Assert.AreEqual(expectedColumn1Name, parser.Slices[0]);
			Assert.AreEqual(expectedColumn2Name, parser.Slices[1]);
			Assert.AreEqual(expectedColumn3Name, parser.Slices[2]);
			Assert.AreEqual(true, parser.HasNumber);
		}

		[TestMethod]
		[ExpectedException(typeof(FileParser.Exception.ColumnCountDoesntMatchException))]
		public void OneColumnExpectTwoColumn()
		{
			var input = RowMaker("One");
			var parser = new RowParser(2);

			parser.Parse(input);
		}

		private string RowMaker(params string[] columns)
		{
			return string.Join(FileContentConsts.ColumnDelimeter,columns);
		}

		private string LineMaker(params string[] lines)
		{
			return string.Join(FileContentConsts.NewLine, lines);
		}
	}
}
