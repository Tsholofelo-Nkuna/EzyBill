import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
 {
   path: 'home',
   loadChildren: () => import("./features/home-page/home-page.module").then(m => m.HomePageModule),
 },
 {
   path: 'manage',
   loadChildren: () => import("./features/manage/manage.module").then(m => m.ManageModule)
 },
 {
   path:'', redirectTo:'home',pathMatch:'full'
 },
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true, preloadingStrategy: PreloadAllModules})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
