import { Component } from '@angular/core';
import { LocationAccessService } from './location-access.service';
import {  delay, mergeMap } from "rxjs/operators";

@Component({
  selector: 'pm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  isvalidCountry:boolean=false;
  
  _countryName:string;
  get countryName():string
  {
    return this._countryName;
  }
  set countryName(value:string)
  {
    this._countryName=value;
  }
  _ipAddress:string;
  get ipAddress():string
  {
    return this._ipAddress;
  }
  set ipAddress(value:string)
  {
    this._ipAddress=value;
  }
  
  errorMessage = '';
  pageTitle = "Let's Read Book Club";
  constructor(private locationService: LocationAccessService){}
  ngOnInit(): void {
    
    this.locationService.getCName().subscribe({
        next:Cname=>{
          this.countryName=Cname.toString();
          if(this.countryName.toLocaleLowerCase()=="india")
          this.isvalidCountry=true;
      },
      error:err => {this.errorMessage = err;
        this.countryName=err.error.text;
        if(this.countryName.toLocaleLowerCase()=="india")
          this.isvalidCountry=true;
        
       }
    });
    
  }
  


}
