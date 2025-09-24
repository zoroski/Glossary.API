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
    public class DeleteTermHendlerTests
    {
        [Fact]
        public async Task Handle_Delete_ByAuthor_Deletes_And_Saves()
        {
            var (repo, uow, auth) = TestHelper.MockResources();


            var authorId = Guid.NewGuid();
            auth.UserId.Returns(authorId);

            var term = Term.Create("name", new string('x', 40), authorId);
            repo.GetByIdAsync(term.Id, Arg.Any<CancellationToken>()).Returns(term);

            var handler = new DeleteTermHendler(repo, uow, auth);

            var ok = await handler.Handle(new DeleteTermComand(term.Id), CancellationToken.None);

            Assert.True(ok);
            repo.Received(1).Delete(term);
            await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_Delete_ByNonAuthor_Throws_And_DoesNotDelete()
        {
            var (repo, uow, auth) = TestHelper.MockResources();

            var authorId = Guid.NewGuid();
            var otherId = Guid.NewGuid();
            auth.UserId.Returns(otherId);

            var term = Term.Create("name", new string('x', 40), authorId);

            repo.GetByIdAsync(term.Id, Arg.Any<CancellationToken>()).Returns(term);

            var handler = new DeleteTermHendler(repo, uow, auth);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await handler.Handle(new DeleteTermComand(term.Id), CancellationToken.None));

            Assert.Equal("Only author can delete own draft.", ex.Message);

            repo.DidNotReceive().Delete(Arg.Any<Term>());
            await uow.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

    }
}
