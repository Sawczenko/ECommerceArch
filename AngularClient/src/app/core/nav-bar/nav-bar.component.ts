import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {IUser} from "../../shared/models/user";
import {AccountService} from "../../account/account.service";
import {BasketService} from "../../basket/basket.service";
import {IBasket} from "../../shared/models/basket";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit{
  currentUser$: Observable<IUser>;
  basket$: Observable<IBasket>;
  constructor(private accountService: AccountService,private basketService: BasketService) {
  }
  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.basket$ = this.basketService.basket$;
  }

  logout(){
    this.accountService.logout();
  }
}
