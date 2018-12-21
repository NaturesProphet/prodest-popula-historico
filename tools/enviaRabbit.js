#!/usr/bin/env node
const rabbitHost = process.env.RABBIT_HOST || 'localhost';
const rabbitTopic = process.env.RABBIT_TOPIC || 'CETURB';
const chave = process.env.CHAVE_DE_ROTEAMENTO || 'realtime.sql';



var amqp = require( 'amqplib/callback_api' );

amqp.connect( `amqp://${rabbitHost}`, function ( err, conn ) {




    conn.createChannel( function ( err, ch ) {
        var topico = rabbitTopic;
        var key = chave;
        var jsn = `{
            "CURSO": 148,
            "ED3_ACIONADA": false,
            "DATAHORA": 1544115750000,
            "IGNICAO": true,
            "ROTULO": "21119",
            "ED4_ACIONADA": false,
            "ED1_ACIONADA": false,
            "ED2_ACIONADA": false,
            "LOCALIZACAO":{
                "type":"Point",
                "coordinates":[ -40.390191666666666, -20.341628333333333]
            }
        }`;
        var msg = process.argv.slice( 2 ).join( ' ' ) || jsn;
        ch.assertExchange( topico, 'topic', { durable: true } );
        ch.publish( topico, key, new Buffer( msg ), { persistent: true } );
        console.log( `[ RABBIT ]   Mensagem "${msg}" enviada ao topico "${topico}" em "${rabbitHost}" com a chave "${key}".\n` )
    } );




    setTimeout( function () { conn.close(); process.exit( 0 ) }, 500 );
} );













// codigo do exemplo do tutorial work-queues
// var amqp = require( 'amqplib/callback_api' );

// amqp.connect( 'amqp://localhost', function ( err, conn ) {
//     conn.createChannel( function ( err, ch ) {
//         var q = 'task_queue';
//         var msg = process.argv.slice( 2 ).join( ' ' ) || "Hello World!";

//         ch.assertQueue( q, { durable: true } );
//         ch.sendToQueue( q, new Buffer( msg ), { persistent: true } );
//         console.log( " [x] Sent '%s'", msg );
//     } );
//     setTimeout( function () { conn.close(); process.exit( 0 ) }, 500 );
// } );
