using Glossary.Application.Interfaces;
using Glossary.Application.Terms.Comands;
using Glossary.Domain.Entities;
using Glossary.Domain.Entities.Spec;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Tests
{
    public class PublishTermmHandlerTests
    {
        [Fact]
        public async Task Handle_Publishes_Term_And_Saves()
        {
            var repo = Substitute.For<ITermRepository>();
            var uow = Substitute.For<IUnitOfWork>();
            var auth = Substitute.For<ICurrentUser>();
            var pubSpec = Substitute.For<IPublishableSpecification>();
            pubSpec.IsSatisfiedBy(Arg.Any<Term>(), out string? reason).Returns(true);
           
            var term = Term.Create("name", new string('x', 40), Guid.NewGuid());
            repo.GetByIdAsync(term.Id, Arg.Any<CancellationToken>()).Returns(term);

            var handler = new PublishTermmHandler(repo, uow, auth, pubSpec);

            var ok = await handler.Handle(new PublishTermComand(term.Id), CancellationToken.None);

            Assert.True(ok);
            await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            await repo.Received(1).GetByIdAsync(term.Id, Arg.Any<CancellationToken>());
        }
    }
}
