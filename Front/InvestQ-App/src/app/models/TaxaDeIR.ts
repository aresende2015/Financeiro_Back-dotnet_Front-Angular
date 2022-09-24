import { Guid } from "guid-typescript";
import { TipoDeAtivo } from "@app/models/Enum/TipoDeAtivo.enum";

export class TaxaDeIR {
  id: Guid;
  tipoDeAtivo: TipoDeAtivo;
  percentual: number;
  dataInicio: Date;
  dataFim: Date;

}
