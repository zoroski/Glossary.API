using Glossary.Application.Interfaces;
using Glossary.Application.Terms.Comands;
using Glossary.Domain.Entities;
using Glossary.Tests.Helpers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Tests
{
    public class CreateTermHandlerTests
    {
        [Fact]
        public async Task Handle_Creates_Term_And_Saves()
        {
            var (repo, uow, auth) = TestHelper.MockResources();

            auth.UserId.Returns(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            var handler = new CreateTermHandler(repo, uow, auth);

            var cmd = new CreateTermComand("abyssal plain", new string('x', 50));
            var id = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, id);
            await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());

            repo.Received(1).Add(Arg.Any<Term>());
        }
    }
}
