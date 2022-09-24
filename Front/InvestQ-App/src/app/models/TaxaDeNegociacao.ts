import { Guid } from "guid-typescript";
import { TipoDeAtivo } from "./Enum/TipoDeAtivo.enum";

export class TaxaDeNegociacao {
  id: Guid;
  tipoDeAtivo: TipoDeAtivo;
  percentual: number;
  dataInicio: Date;
  dataFim: Date;

}
