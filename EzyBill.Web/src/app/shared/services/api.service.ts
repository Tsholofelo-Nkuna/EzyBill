import { HttpClient } from "@angular/common/http";
import { isDevMode, inject} from "@angular/core"


export class ApiService{
  private _host = ''
  protected httpClient = inject(HttpClient);
  public url: string = '';
  constructor(url: string){
    if(isDevMode()){
      this._host = 'https://localhost:7241'
    }
    else{
      this._host = 'https://';
    }
    this.url = `${this._host}/${url}`;
  }
}
