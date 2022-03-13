import {v4 as uuidv4} from 'uuid'
export interface IBasket{
  id: string;
  totalPrice: number
  basketItems: IBasketItem[];
  couponCode: ICouponCode;
}
export interface  IBasketItem{
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export class Basket implements IBasket{
  id = uuidv4();
  totalPrice;
  basketItems : IBasketItem[] = [] ;
  couponCode= new Coupon();
}

export class Coupon implements ICouponCode{
  code: string;
  codeType: number;
  id: number;
}

export interface IBasketTotals{
  shipping: number;
  subtotal: number;
  total: number;
}
export interface ICouponCode{
  id: number;
  code: string;
  codeType: number;
}
