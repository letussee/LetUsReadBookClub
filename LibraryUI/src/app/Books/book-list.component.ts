import { Component, OnInit } from '@angular/core';

import { cBook,cAuthor } from './book-Author';
import { bookService } from './book.service';


@Component({
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class bookListComponent implements OnInit {
  pageTitle = 'Book List';
  errorMessage = '';

  _listFilter = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredbooks = this.listFilter ?(this.selectedFilterName.toLocaleLowerCase()=="book title"? this.performFilterByBookTitle(this.listFilter)
    :this.performFilterByAuthor(this.listFilter)) : this.books;
  }
  selected(){
    console.log(this.selectedFilterName);
    
  }
 _selectedFilterName:string="Book Title";
  get selectedFilterName():string
  {
    return this._selectedFilterName;
  }
  set selectedFilterName(value:string)
  {
    this._selectedFilterName=value;
  }

  selectedFilterOption: any[] = [
    { id: 1, name: 'Book Title'},
    { id: 2, name: 'Author' }
    
  ];
  
  filteredbooks: cBook[] = [];
  books: cBook[] = [];
  

  constructor(private bookService: bookService) { }

    performFilterByBookTitle(filterBy: string): cBook[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.books.filter((book: cBook) =>
      book.title.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  performFilterByAuthor(filterBy: string): cBook[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.books.filter((book: cBook) =>
      book.authors.some(auth=>auth.authorName.toLocaleLowerCase().indexOf(filterBy) !== -1) 
      );
  }
  
  ngOnInit(): void {
    this.bookService.getbooks().subscribe({
      next: books => {
        this.books = books;
        
        this.filteredbooks = this.books;
        
      },
      error: err => this.errorMessage = err
    });
    
  }
}
