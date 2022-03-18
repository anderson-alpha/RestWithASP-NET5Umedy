using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        //SERVE PARA MOCKAR VALOR
        //private volatile int count;
        //private readonly IBookRepository _repository; // Antes da implementacao generic

        private readonly IRepository<Book> _repository; // Antes da implementacao generic

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);

            return _converter.Parse(bookEntity);
        }
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from books b where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and b.title like '%{name}%' ";
            query += $" order by b.title {sort} offset {offset} rows fetch next {size} rows only";

            string countQuery = @"select * from books b where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and b.title like '%{name}%' ";

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
