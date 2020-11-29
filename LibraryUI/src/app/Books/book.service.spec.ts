import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { bookService } from './book.service';

describe('bookService', () => {
  let bookService: bookService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        bookService
      ],
    });

    bookService = TestBed.get(bookService);
    httpMock = TestBed.get(HttpTestingController);
  });

  it(`should fetch books as an Observable`, async(inject([HttpTestingController, bookService],
    (httpClient: HttpTestingController, bookService: bookService) => {

      const bookList = [{"bookID":1,"isbn":"TKMB","title":"To kill a mocking bird","bookStatus":"Available","authors":[{"authorID":1,"authorName":"Harper Lee","description":"","books":null}]},{"bookID":2,"isbn":"GOMMN","title":"Good Omens","bookStatus":"Available","authors":[{"authorID":4,"authorName":"Neil German","description":"","books":null},{"authorID":5,"authorName":"Terry Pratchett","description":"","books":null}]},{"bookID":3,"isbn":"HYLS","title":"Heads You loose","bookStatus":"Rented","authors":[{"authorID":2,"authorName":"Joseph Helller","description":"","books":null},{"authorID":6,"authorName":"David Hayward","description":"","books":null}]},{"bookID":4,"isbn":"HTFY","title":"The hands that feed you","bookStatus":"Available","authors":[{"authorID":4,"authorName":"Neil German","description":"","books":null}]},{"bookID":5,"isbn":"TLOR","title":"The Lord of the rings","bookStatus":"Available","authors":[{"authorID":3,"authorName":"J R Tolkien","description":"","books":null}]}];


      bookService.getbooks()
        .subscribe((books: any) => {
          expect(books.length).toBe(3);
        });

      let req = httpMock.expectOne('http://localhost:61603/api/Books');
      expect(req.request.method).toBe("GET");

      req.flush(bookList);
      httpMock.verify();

    })));
});