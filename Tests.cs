using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace SaucedemoTests
{
    [TestFixture]
    public class SaucedemoTestSuite
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Инициализация драйвера Chrome
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            // Закрытие браузера и освобождение ресурсов
            driver.Quit();
        }

        [Test]
        public void VerifySuccessfulLogin()
        {
            // Открытие страницы авторизации
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Ввод логина и пароля
            IWebElement usernameField = driver.FindElement(By.Id("user-name"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameField.SendKeys("standard_user");
            passwordField.SendKeys("secret_sauce");
            loginButton.Click();

            // Проверка, что пользователь успешно авторизовался
            Assert.IsTrue(driver.Url.Contains("inventory.html"));
        }

        [Test]
        public void VerifyAddAndEditOrder()
        {
            // Открытие страницы авторизации
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Ввод логина и пароля
            IWebElement usernameField = driver.FindElement(By.Id("user-name"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameField.SendKeys("standard_user");
            passwordField.SendKeys("secret_sauce");
            loginButton.Click();

            // Проверка, что пользователь успешно авторизовался
            Assert.IsTrue(driver.Url.Contains("inventory.html"));

            // Добавление товара в корзину
            IWebElement addToCartButton = driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            addToCartButton.Click();

            // Переход в корзину
            IWebElement cartLink = driver.FindElement(By.ClassName("shopping_cart_link"));
            cartLink.Click();

            // Проверка, что товар добавлен в корзину
            IWebElement cartItem = driver.FindElement(By.ClassName("cart_item"));
            Assert.IsTrue(cartItem.Displayed);

            // Переход к оформлению заказа
            IWebElement checkoutButton = driver.FindElement(By.Id("checkout"));
            checkoutButton.Click();

            // Заполнение информации для оформления заказа
            IWebElement firstNameField = driver.FindElement(By.Id("first-name"));
            IWebElement lastNameField = driver.FindElement(By.Id("last-name"));
            IWebElement postalCodeField = driver.FindElement(By.Id("postal-code"));
            IWebElement continueButton = driver.FindElement(By.Id("continue"));

            firstNameField.SendKeys("John");
            lastNameField.SendKeys("Doe");
            postalCodeField.SendKeys("12345");
            continueButton.Click();

            // Проверка, что переход к подтверждению заказа успешен
            Assert.IsTrue(driver.Url.Contains("checkout-step-two.html"));

            // Удаление товара из корзины
            IWebElement removeButton = driver.FindElement(By.Id("remove-sauce-labs-backpack"));
            removeButton.Click();

            // Проверка, что товар удален из корзины
            var cartItems = driver.FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(0, cartItems.Count);

            // Переход к оформлению заказа с пустой корзиной
            IWebElement continueShoppingButton = driver.FindElement(By.Id("continue-shopping"));
            continueShoppingButton.Click();

            // Проверка, что пользователь вернулся на страницу товаров
            Assert.IsTrue(driver.Url.Contains("inventory.html"));
        }

        [Test]
        public void VerifyOrderCompletion()
        {
            // Открытие страницы авторизации
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Ввод логина и пароля
            IWebElement usernameField = driver.FindElement(By.Id("user-name"));
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameField.SendKeys("standard_user");
            passwordField.SendKeys("secret_sauce");
            loginButton.Click();

            // Проверка, что пользователь успешно авторизовался
            Assert.IsTrue(driver.Url.Contains("inventory.html"));

            // Добавление товара в корзину
            IWebElement addToCartButton = driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            addToCartButton.Click();

            // Переход в корзину
            IWebElement cartLink = driver.FindElement(By.ClassName("shopping_cart_link"));
            cartLink.Click();

            // Проверка, что товар добавлен в корзину
            IWebElement cartItem = driver.FindElement(By.ClassName("cart_item"));
            Assert.IsTrue(cartItem.Displayed);

            // Переход к оформлению заказа
            IWebElement checkoutButton = driver.FindElement(By.Id("checkout"));
            checkoutButton.Click();

            // Заполнение информации для оформления заказа
            IWebElement firstNameField = driver.FindElement(By.Id("first-name"));
            IWebElement lastNameField = driver.FindElement(By.Id("last-name"));
            IWebElement postalCodeField = driver.FindElement(By.Id("postal-code"));
            IWebElement continueButton = driver.FindElement(By.Id("continue"));

            firstNameField.SendKeys("John");
            lastNameField.SendKeys("Doe");
            postalCodeField.SendKeys("12345");
            continueButton.Click();

            // Проверка, что переход к подтверждению заказа успешен
            Assert.IsTrue(driver.Url.Contains("checkout-step-two.html"));

            // Подтверждение заказа
            IWebElement finishButton = driver.FindElement(By.Id("finish"));
            finishButton.Click();

            // Проверка, что заказ успешно оформлен
            IWebElement completeHeader = driver.FindElement(By.ClassName("complete-header"));
            Assert.AreEqual("Thank you for your order!", completeHeader.Text);
        }
    }
}