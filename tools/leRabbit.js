#!/usr/bin/env node
const rabbitHost = process.env.RABBIT_HOST || 'localhost';
const rabbitTopic = process.env.RABBIT_TOPIC || 'CETURB';
const chave = process.env.CHAVE_DE_ROTEAMENTO || '#';



var amqp = require( 'amqplib/callback_api' );

var keys = [ chave ];

amqp.connect( `amqp://${rabbitHost}`, function ( err, conn ) {
    conn.createChannel( function ( err, ch ) {
        var topico = rabbitTopic;

        ch.assertExchange( topico, 'topic', { durable: true, persistent: true } );

        ch.assertQueue( 'CETURB', { persistent: true }, function ( err, q ) {
            console.log( `[ RABBIT LISTENER ]   Ouvindo o topico ${rabbitTopic} no servidor ${rabbitHost} usando as keys: ${keys}\n` );

            keys.forEach( function ( key ) {
                ch.bindQueue( q.queue, topico, key );
            } );

            ch.consume( q.queue, function ( msg ) {
                console.log( `[ RABBIT LISTENER ]   Key: "${msg.fields.routingKey}". Mensagem: "${msg.content.toString()}".` );
            }, { noAck: false } );
        } );
    } );
} );










// codigo do exemplo do tutorial work-queues
// var amqp = require( 'amqplib/callback_api' );

// amqp.connect( 'amqp://localhost', function ( err, conn ) {
//     conn.createChannel( function ( err, ch ) {
//         var q = 'task_queue';

//         ch.assertQueue( q, { durable: true } );
//         //ch.prefetch( 1 );
//         console.log( " [*] Waiting for messages in %s. To exit press CTRL+C", q );
//         ch.consume( q, function ( msg ) {
//             var secs = msg.content.toString().split( '.' ).length - 1;

//             console.log( " [x] Received %s", msg.content.toString() );
//             setTimeout( function () {
//                 console.log( " [x] Done" );
//                 //ch.ack( msg );
//             }, 500 );
//         }, { noAck: false } );
//     } );
// } );
