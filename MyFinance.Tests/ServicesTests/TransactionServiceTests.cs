using Microsoft.AspNetCore.Identity;
using Moq;
using MyFinance.Entities;
using MyFinance.Models.Transaction;
using MyFinance.Repositories;
using MyFinance.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyFinance.Tests
{
    public class TransactionServiceTests
    {
        private Mock<IRepository<Transaction>> mockedTransactionRepository;
        private Mock<IUserStore<User>> mockedUserStore;
        private Mock<UserManager<User>> mockedUserManager;
        private Mock<IRepository<Account>> mockedAccountRepository;

        public TransactionServiceTests()
        {
            mockedTransactionRepository = new Mock<IRepository<Transaction>>();
            mockedUserStore = new Mock<IUserStore<User>>();
            mockedUserManager = new Mock<UserManager<User>>(mockedUserStore.Object, null, null,
                                                                null, null, null, null, null, null);
            mockedAccountRepository = new Mock<IRepository<Account>>();
        }
        [Fact]
        public void CalculateExpansesTest_1()
        {
            //Arange
            var transactionService = new TransactionService(mockedTransactionRepository.Object, 
                                                            mockedAccountRepository.Object, 
                                                            mockedUserManager.Object);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 50
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 100.5m
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 200
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 300
                },
            };

            //Act
            var actual = transactionService.CalculateExpanses(transactions);
            var expected = 0;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateExpansesTest_2()
        {
            //Arange
            var transactionService = new TransactionService(mockedTransactionRepository.Object,
                                                            mockedAccountRepository.Object,
                                                            mockedUserManager.Object);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 50
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 100.5m
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 200
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 300
                },
            };

            //Act
            var actual = transactionService.CalculateExpanses(transactions);
            var expected =400.5m;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateEarningsTest_1()
        {
            //Arange
            var transactionService = new TransactionService(mockedTransactionRepository.Object,
                                                            mockedAccountRepository.Object,
                                                            mockedUserManager.Object);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 50
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 100.5m
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 200
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 300
                },
            };

            //Act
            var actual = transactionService.CalculateEarnings(transactions);
            var expected = 0;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateEarningsTest_2()
        {
            //Arange
            var transactionService = new TransactionService(mockedTransactionRepository.Object,
                                                            mockedAccountRepository.Object,
                                                            mockedUserManager.Object);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 50
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 100.5m
                },
                new Transaction
                {
                    IsExpanse=true,
                    Amount = 200
                },
                new Transaction
                {
                    IsExpanse=false,
                    Amount = 300
                },
            };

            //Act
            var actual = transactionService.CalculateEarnings(transactions);
            var expected = 400.5m;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddTransactionTest_ShouldWork()
        {
            //Arange
            var model = new CreateViewModel
            {
                AccountId = 5,
                CategoryId = 5,
                DateTime = "24.02.1997",
                Amount = 20,
                Type = Models.Enums.TransactionType.Expanse
            };

            mockedTransactionRepository.Setup(r => r.Save()).Returns(true);

            var transactionService = new TransactionService(mockedTransactionRepository.Object,
                                                            mockedAccountRepository.Object,
                                                            mockedUserManager.Object);
            //Act
            var expected = new Transaction
            {
                AccountId = 5,
                CategoryId = 5,
                DateTime = new DateTime(1997, 2, 24),
                Amount = 20,
                IsExpanse = true
            };
            var actual = transactionService.AddTransaction(model);
            //Assert
            Assert.Equal(expected.AccountId, actual.AccountId);
            Assert.Equal(expected.CategoryId, actual.CategoryId);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.IsExpanse, actual.IsExpanse);
              
        }
       
    }
}
        