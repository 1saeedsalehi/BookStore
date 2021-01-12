import { Component,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { debug } from 'console';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public listings: Listings[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Listings[]>(baseUrl + 'Listing').subscribe(result => {
      debugger;
      this.listings = result;
    }, error => console.error(error));
  }
}

interface Listings {
  listingTitle: string;
}
