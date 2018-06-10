using System;
using System.Collections.Generic;
using System.Text;
using friendManager.Controllers;
using friendManager.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace friendManagerTests
{
    public class FriendControllerTest
    {

        [Fact]
        public void AddingFriendworks() {

            //Arrange
            var mockContext = new Mock<friendManagerContext>();
        
            FriendsController controller = new FriendsController(mockContext.Object);

            var friend = new Friend
            {
                Name = "Maciej Maciejowski",
                FirstMeetDate = DateTime.Parse("2012-12-12 14:53:00"),
                BiggestSecret = "Dislikes Charmander",
                Likeability = 12.12M
            };

            //Act
            var result = controller.Create(friend);
            mockContext.Verify(ctx => ctx.Add(friend));

            
        }

    }
}
