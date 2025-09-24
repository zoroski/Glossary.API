using Glossary.Application.Interfaces;
using Glossary.Domain.Entities;
using Glossary.Domain.Entities.Spec;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Tests.Helpers
{
    public static class TestHelper
    {
        public static (ITermRepository repo, IUnitOfWork uow, ICurrentUser auth)
            MockResources(Guid? userId = null)
        {
            var repo = Substitute.For<ITermRepository>();
            var uow = Substitute.For<IUnitOfWork>();
            var auth = Substitute.For<ICurrentUser>();

            if (userId.HasValue)
                auth.UserId.Returns(userId.Value);

            return (repo, uow, auth);
        }

        public static IPublishableSpecification AlwaysPublishable()
        {
            var spec = Substitute.For<IPublishableSpecification>();
            spec.IsSatisfiedBy(Arg.Any<Term>(), out string? reason).Returns(true);
            return spec;
        }

        public static Term Draft(string name = "term", string? definition = null, Guid? authorId = null)
        {
            return Term.Create(
                name,
                definition ?? new string('x', 50),
                authorId ?? Guid.NewGuid());
        }

        public static Term Published(string name = "term", string? definition = null, Guid? authorId = null)
        {
            var term = Draft(name, definition, authorId);
            term.Publish(AlwaysPublishable());
            return term;
        }
    }
    }
