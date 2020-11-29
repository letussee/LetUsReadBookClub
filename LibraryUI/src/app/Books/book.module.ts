import { NgModule } from '@angular/core';
import { bookListComponent } from './book-list.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    bookListComponent
  ],
  imports: [
    RouterModule.forChild([
      { path: 'books', component: bookListComponent },
      {
        // Place holder for book items
        path: 'books/:id',component: bookListComponent
      }
    ]),
    SharedModule
  ]
})
export class bookModule { }
