using FluentAssertions;
using Moq;
using Vidly.BusinessLogic.Movies;
using Vidly.BusinessLogic.Movies.Entities;

namespace Vidly.BusinessLogic.Test
{
    [TestClass]
    public sealed class MovieServiceTest
    {
        private Mock<IMovieRepository> _movieRepositoryMock;
        private MovieService _movieService;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
            _movieService = new MovieService(_movieRepositoryMock.Object);
        }

        #region Create
        #region Error
        [TestMethod]
        public void Create_WhenTitleIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieArgs(
                "Duplicated",
                "valid description",
                DateTimeOffset.UtcNow.AddMonths(-1));

            _movieRepositoryMock
                .Setup(mock => mock.Exists(movie => movie.Title == args.Title))
                .Returns(true);

            var act = () => _movieService.Add(args);

            act.Should().Throw<Exception>().WithMessage("Movie duplicated");
            _movieRepositoryMock.VerifyAll();
        }
        #endregion

        #region Success

        [TestMethod]
        public void Create_WhenCorrectInfo_ShouldCreateMovie()
        {
            var args = new CreateMovieArgs(
                "some name",
                "some description",
                DateTimeOffset.UtcNow);


            _movieRepositoryMock
                .Setup(mock => mock.Exists(movie => movie.Title == args.Title))
                .Returns(false);

            _movieRepositoryMock.
                Setup(mock => mock.Create(It.Is<Movie>( m =>
                m.Title == args.Title
                && m.Description == args.Description
                && m.PublishedOn == args.PublishedOn)));

            var response = _movieService.Add(args);

            _movieRepositoryMock.VerifyAll();
            response.Should().NotBeNull();
            response.Id.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
        }
        #endregion
        #endregion
    }
}
