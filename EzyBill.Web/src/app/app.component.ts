import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationStart, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit, OnDestroy{

  title = 'EzyBill.Web';
  username = "tgnkuna768@gmail.com"
  sidebarMenuItems: SidebarMenuItem[] = [
    new SidebarMenuItem("Home", [], "/home"),
    new SidebarMenuItem("Setup", [
      {title: "Tax",children:[],link: "/"},
      {title: "Discounts",children:[],link: "/"},
      {title: "Business Details",children:[],link: "/"},
    ], "/#"),
    new SidebarMenuItem("Manage", [
     // {title: "Accounts",children:[],link: "/manage/accounts"},
      {title: "Customers",children:[],link: "/manage/customers"},
      {title: "Payments",children:[],link: "/manage/payments"},
      {title: "Invoices",children:[],link: "/manage/invoices"},
    ], "/"),
  ];
  selectedSideBarItemId: string | null = '';
  subscriptions = new Subscription();
  breadcrumbItems: string[] = [];
  constructor(private _router: Router, private _route: ActivatedRoute){
    
  }
    ngOnDestroy(): void {
       this.subscriptions.unsubscribe();
    }
   
    ngOnInit(): void {
      this.subscriptions.add(this._router.events.subscribe(e => {
           if(e instanceof NavigationStart){
             this.breadcrumbItems = e.url.split("/").filter(x => x.trim() != "");
             if(this.breadcrumbItems.length == 1 && this.breadcrumbItems[0].toLocaleLowerCase() === 'home'){
               this.breadcrumbItems = [];
             }
             var d = this._route.firstChild?.snapshot.data;
           }
      }));
    }
   onSidebarMenuItemClick(elementId: string | null){
     this.selectedSideBarItemId = elementId;
   }
}
class SidebarMenuItem{
  
   constructor(public title:string, public children: SidebarMenuItem[], public link:string){}
}
