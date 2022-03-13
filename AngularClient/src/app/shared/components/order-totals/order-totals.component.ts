import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {Basket, IBasket, IBasketTotals} from "../../models/basket";
import {BasketService} from "../../../basket/basket.service";

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  basketTotal$: Observable<IBasketTotals>;
  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketTotal$ = this.basketService.basketTotal$;
    this.basket$ = this.basketService.basket$
  }

}
