/* ====================================================================
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for Additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
==================================================================== */
namespace TestCases.HSSF.UserModel
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using NPOI.HSSF.UserModel;
    using TestCases.HSSF;
    using NPOI.SS.UserModel;
    using TestCases.SS.UserModel;
    /**
     * Tests TestHSSFCellComment.
     *
     * @author  Yegor Kozlov
     */
    [TestFixture]
    public class TestHSSFComment:BaseTestCellComment
    {
        public TestHSSFComment(): base(HSSFITestDataProvider.Instance)
        {

        }

        [Test]
        public void TestDefaultShapeType()
        {
            HSSFComment comment = new HSSFComment((HSSFShape)null, new HSSFClientAnchor());
            Assert.AreEqual(HSSFSimpleShape.OBJECT_TYPE_COMMENT, comment.ShapeType);
        }
        /**
 *  HSSFCell#findCellComment should NOT rely on the order of records
 * when matching cells and their cell comments. The correct algorithm is to map
 */
        [Test]
        public void Test47924()
        {
            HSSFWorkbook wb = HSSFTestDataSamples.OpenSampleWorkbook("47924.xls");
            ISheet sheet = wb.GetSheetAt(0);
            ICell cell;
            IComment comment;

            cell = sheet.GetRow(0).GetCell(0);
            comment = cell.CellComment;
            Assert.AreEqual("a1", comment.String.String);

            cell = sheet.GetRow(1).GetCell(0);
            comment = cell.CellComment;
            Assert.AreEqual("a2", comment.String.String);

            cell = sheet.GetRow(2).GetCell(0);
            comment = cell.CellComment;
            Assert.AreEqual("a3", comment.String.String);

            cell = sheet.GetRow(2).GetCell(2);
            comment = cell.CellComment;
            Assert.AreEqual("c3", comment.String.String);

            cell = sheet.GetRow(4).GetCell(1);
            comment = cell.CellComment;
            Assert.AreEqual("b5", comment.String.String);

            cell = sheet.GetRow(5).GetCell(2);
            comment = cell.CellComment;
            Assert.AreEqual("c6", comment.String.String);
        }

        [Test]
        public void TestGetClientAnchor()
        {
            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet sheet = wb.CreateSheet() as HSSFSheet;
            HSSFPatriarch Drawing = sheet.CreateDrawingPatriarch() as HSSFPatriarch;
            HSSFComment comment;
            IClientAnchor anchor;

            comment = Drawing.CreateCellComment(new HSSFClientAnchor(101, 102, 103, 104, (short)1, 2, (short)3, 4))
                as HSSFComment;
            anchor = comment.ClientAnchor;
            Assert.AreEqual(101, anchor.Dx1);
            Assert.AreEqual(102, anchor.Dy1);
            Assert.AreEqual(103, anchor.Dx2);
            Assert.AreEqual(104, anchor.Dy2);
            Assert.AreEqual(1, anchor.Col1);
            Assert.AreEqual(2, anchor.Row1);
            Assert.AreEqual(3, anchor.Col2);
            Assert.AreEqual(4, anchor.Row2);

            comment = Drawing.CreateCellComment(new HSSFClientAnchor()) as HSSFComment;
            anchor = comment.ClientAnchor;
            Assert.AreEqual(0, anchor.Dx1);
            Assert.AreEqual(0, anchor.Dy1);
            Assert.AreEqual(0, anchor.Dx2);
            Assert.AreEqual(0, anchor.Dy2);
            Assert.AreEqual(0, anchor.Col1);
            Assert.AreEqual(0, anchor.Row1);
            Assert.AreEqual(0, anchor.Col2);
            Assert.AreEqual(0, anchor.Row2);
        }

    }
}
