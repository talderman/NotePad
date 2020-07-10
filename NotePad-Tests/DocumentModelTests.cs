using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NotePad_Tests
{
    [TestClass]
    public class DocumentModelTests
    {
        [TestMethod]
        public void TextPropertyReturnsCorrectValue()
        {
            var documentModelTextValue = "klasdfi02-3k=e-ruijber";
            var documentModel = new NotePad.Models.DocumentModel { Text = documentModelTextValue };
            Assert.AreEqual(documentModel.Text, documentModelTextValue);
        }

    }
}
