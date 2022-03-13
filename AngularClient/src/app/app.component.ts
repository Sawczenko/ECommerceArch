import {Component, OnInit} from '@angular/core';
import {AccountService} from "./account/account.service";
import {BasketService} from "./basket/basket.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'AngularClient';

  constructor(private accountService: AccountService, private basketService: BasketService) {
  }

  ngOnInit(): void {
    this.loadCurrentUser();
    const basketId = localStorage.getItem('basket_id');
    if(basketId){
      this.basketService.getBasket(basketId).subscribe(() =>{
        console.log('Initialized basket')
      },error => {
        console.log(error);
      });
    }
  }
  loadCurrentUser(){
    const token = localStorage.getItem('token');
    if(token){
      this.accountService.loadCurrentUser(token).subscribe(() =>
      console.log('loaded user'),error => {
        console.log(error)
      })
    }
  }
}
