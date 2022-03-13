import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject} from "rxjs";
import {Basket, Coupon, IBasket, IBasketItem, IBasketTotals} from "../shared/models/basket";
import {map} from "rxjs/operators";
import {IProduct} from "../shared/models/product";

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.basketService;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null)
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    let headers = new HttpHeaders();
    console.log("X")
    headers = headers.set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get(this.baseUrl+'basket?id='+id,{headers})
      .pipe(map((basket:IBasket) => {
        this.basketSource.next(basket);
        this.calculateTotals()
      }));
  }
  setBasket(basket: IBasket){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.post(this.baseUrl,basket,{headers}).subscribe((response: IBasket)=>{
      this.basketSource.next(response);
      this.calculateTotals()
    }, error => {
      console.log(error)
    })
  }
  getCurrentBasketValue(){
    return this.basketSource.value;
  }
  addItemToBasket(item: IProduct,quantity = 1 ){
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item,quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, itemToAdd,quantity);
    this.setBasket(basket);
  }

  private calculateTotals(){
    const basket = this.getCurrentBasketValue()
    const shipping =0;
    const subtotal = basket.totalPrice;
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping,total,subtotal})
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[]{
    const index = items.findIndex(i=> i.id === itemToAdd.id);
    if(index === -1){
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }else{
      items[index].quantity += quantity;
    }
    return items;
  }
  private createBasket():IBasket{
    const basket = new Basket();
    localStorage.setItem('basket_id',basket.id);
    return basket;
  }
  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem{
    return {
      id: item.id,
      productName: item.productName,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    }
  }
   incrementItemQuantity(item: IBasketItem){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(x=> x.id == item.id);
    basket.basketItems[foundItemIndex].quantity++;
    this.setBasket(basket);
  }
   decrementItemQuantity(item: IBasketItem){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(x=> x.id === item.id);
    if(basket.basketItems[foundItemIndex].quantity>1){
      basket.basketItems[foundItemIndex].quantity--;
      this.setBasket(basket);
    }
    else{
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue()
    if(basket.basketItems.some(x=> x.id==item.id)){
      basket.basketItems = basket.basketItems.filter(i=> i.id!=item.id)
      if(basket.basketItems.length > 0){
        this.setBasket(basket);
      }
      else{
        this.deleteBasket(basket);
      }
    }
  }

  private deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id='+basket.id).subscribe(()=>{
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    },error => {
      console.log(error);
    })
  }
  applyCouponCode(couponCode: string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    let basket = this.getCurrentBasketValue();
    basket.couponCode = new Coupon();
    basket.couponCode.code = couponCode;
    return this.http.post(this.baseUrl+"applycode",basket,{headers}).subscribe((response: IBasket)=>
    {
      this.basketSource.next(response);
      this.calculateTotals();
    }, error => {
      console.log(error);
    });
  }
}
