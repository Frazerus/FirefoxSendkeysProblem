using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

var mainDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

var firefoxOptions = new FirefoxOptions();
firefoxOptions.LogLevel = FirefoxDriverLogLevel.Trace;
var driver = new FirefoxDriver(mainDirectory + @"\driver\", firefoxOptions);

var url = Path.Combine(mainDirectory, "index.html");
driver.Navigate().GoToUrl(url);

var element = driver.FindElement(By.Id("textInput"));

element.SendKeys("inputWithoutDelete");

Debug.Assert(element.GetAttribute("value") == "inputWithoutDelete", "element.Value == 'inputWithoutDelete'");

var deleteInput = Keys.Control + 'a' + Keys.Control + Keys.Delete;
var valueInput = "myInput";

//This SHOULD already input the correct value, right?
element.SendKeys(deleteInput + valueInput);

Debug.Assert(element.GetAttribute("value") != valueInput, "element.Value != inputValue");
Debug.Assert(element.GetAttribute("value") == string.Empty, "element.Value == string.Empty");

//This actually does what is required.
element.SendKeys(deleteInput);
element.SendKeys(valueInput);

Debug.Assert(element.GetAttribute("value") == valueInput, "element.Value == inputValue");