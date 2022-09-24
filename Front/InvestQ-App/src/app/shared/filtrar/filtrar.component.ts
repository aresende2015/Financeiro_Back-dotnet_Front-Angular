// ## Colocar no componente pai
// public onFiltroAcionado(evento: any) {
//   this.filtrarClientes(evento.filtro)   ou this.filtroLista = evento.filtro;
// }
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-filtrar',
  templateUrl: './filtrar.component.html',
  styleUrls: ['./filtrar.component.scss']
})
export class FiltrarComponent implements OnInit {

  @Input() rotaLink: string = '';
  @Input() botaoNovo = true;
  @Input() filtro = true;
  @Input() placerHolder: string = '';

  @Output() filtroAcionado = new EventEmitter();

  filtrar(evt: any) {
    this.filtroAcionado.emit({filtro: evt.value});
  }

  novo() {
    this.router.navigate([this.rotaLink]);
  }

  constructor(private router: Router,) { }

  ngOnInit(): void {
  }

}
