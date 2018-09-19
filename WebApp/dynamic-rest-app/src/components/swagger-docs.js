import React, {Component} from 'react' ;
import SwaggerCard from './swagger-card';

class SwaggerDoc extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() { 
        return ( <div className="col-md-offset-2 col-md-8 col-sm-12" style={styleContainer}>
              <SwaggerCard 
                title="Swagger API"
                description="description"
                url="http://localhost:5000/swagger"/>
              <SwaggerCard 
                title="Dynamic Swagger"
                description="description"
                url="http://localhost:5000/Dynamic/index.html"/>
        </div> );
    }
}

const styleContainer = {
    paddingTop: 10
}
 
export default SwaggerDoc;