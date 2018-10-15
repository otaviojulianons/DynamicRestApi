//import { formatarData } from '../../../helpers/Formatters';

class Attribute{
  constructor({
    name, 
    dataType, 
    length, 
    allowNull
  }) {
    this.name = name;
    this.dataType = dataType;
    this.length = length;
    this.allowNull = allowNull;
  }

}
export default Attribute;

