export class cBook {
  bookID: number;
  title: string;
  isbn: string;
  bookStatus: string;
  authors:cAuthor[];
}
export class cAuthor
{
  authorID: number;
  authorName: string;
  description: string;
}