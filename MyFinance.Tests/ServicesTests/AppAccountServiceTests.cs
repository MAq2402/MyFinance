﻿using Microsoft.AspNetCore.Identity;
using Moq;
using MyFinance.Entities;
using MyFinance.Models.Transaction;
using MyFinance.Repositories;
using MyFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace MyFinance.Tests.ServicesTests
{
    public class AppAccountServiceTests
    {
        private Mock<IRepository<Transaction>> mockedTransactionRepository;
        private Mock<IUserStore<User>> mockedUserStore;
        private Mock<UserManager<User>> mockedUserManager;
        private Mock<IRepository<Account>> mockedAccountRepository;

        public AppAccountServiceTests()
        {
            mockedTransactionRepository = new Mock<IRepository<MyFinance.Entities.Transaction>>();
            mockedUserStore = new Mock<IUserStore<User>>();
            mockedUserManager = new Mock<UserManager<User>>(mockedUserStore.Object, null, null,
                                                                null, null, null, null, null, null);
            mockedAccountRepository = new Mock<IRepository<Account>>();
        }

        [Fact]
        public void UpdateAmount_ShouldThrowException()
        {
            //Arange
            mockedAccountRepository.Setup(r => r.GetSingleBy(x => false)).Returns<Account>(null);

            var accountSerivce = new AppAccountService(mockedAccountRepository.Object,
                                                       mockedUserManager.Object,
                                                       mockedTransactionRepository.Object);
            var model = new CreateViewModel();
            //Act
            Action action = () => accountSerivce.UpdateAmount(model);
            //Assert
            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void UpdateAmount_ShouldReturnFalse()
        {
            //Arange
            var account = new Account
            {
                Amount = 1
            };
            var model = new CreateViewModel
            {
                Amount = 5,
                 Type=Models.Enums.TransactionType.Expanse
            };
            mockedAccountRepository.Setup(r=>r.GetSingleBy(It.IsAny<Expression<Func<Account,bool>>>()))
                                   .Returns(account);

            var accountSerivce = new AppAccountService(mockedAccountRepository.Object,
                                                       mockedUserManager.Object,
                                                       mockedTransactionRepository.Object);
           
            //Act
            var expected = false;
            var actual = accountSerivce.UpdateAmount(model);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UpdateAmount_ShouldReturnTrue1()
        {
            //Arange
            var account = new Account
            {
                Amount = 10
            };
            var model = new CreateViewModel
            {
                Amount = 5,
                Type = Models.Enums.TransactionType.Expanse
            };
            mockedAccountRepository.Setup(r => r.GetSingleBy(It.IsAny<Expression<Func<Account, bool>>>()))
                                   .Returns(account);

            var accountSerivce = new AppAccountService(mockedAccountRepository.Object,
                                                       mockedUserManager.Object,
                                                       mockedTransactionRepository.Object);

            //Act
            var expected = true;
            var actual = accountSerivce.UpdateAmount(model);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UpdateAmount_ShouldReturnTrue2()
        {
            //Arange
            var account = new Account
            {
                Amount = 1
            };
            var model = new CreateViewModel
            {
                Amount = 5,
                Type = MyFinance.Models.Enums.TransactionType.Earning
            };
            mockedAccountRepository.Setup(r => r.GetSingleBy(It.IsAny<Expression<Func<Account, bool>>>()))
                                   .Returns(account);

            var accountSerivce = new AppAccountService(mockedAccountRepository.Object,
                                                       mockedUserManager.Object,
                                                       mockedTransactionRepository.Object);

            //Act
            var expected = true;
            var actual = accountSerivce.UpdateAmount(model);
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
