# AMQP-based message router

This is a proof-of-concept of a simple message router infrastructure in a distributed systems architecture.

Terminology is inspired from Domain Driven Design (DDD), which is why you'll find concepts like e.g. "Bounded Context" or "Published Language".

# Design objectives

The purpose of this proof-of-concept is to:

- Evaluate AMQP as a potential brokering protocol
- Implement a standards-based solution, that is independent from the broker platform vendor
- Illustrate certain DDD concepts in a distributed systems architecture

## AMQP 0.9.1

The AMQP 0.9.1 version is based on the [EasyNetQ library](https://github.com/EasyNetQ) and the AMQP platform support is handled by [RabbitMQ](https://www.rabbitmq.com).

## AMQP 1.0

The AMQP 1.0 version of this sample is available in the `AMQP1-0` folder. AMQP 1.0 is very different from prior versions (it's essentially a complete rewrite), so some of the terminology is different in this sample as well. 

The goal of the 1.0 sample is to be able to seamlessly switch between Apache MQ and Azure Servicebus without requiring code changes.

The AMQP 1.0 sample is implemented on top of the [AMQP.NET Lite library](https://github.com/Azure/amqpnetlite) by Microsoft

# Architecture of the samples

The samples consists of two bounded contexts, exchanging messages over a message broker:

```
┌───────────────────┐  ┌────────────────────┐   ┌───────────────────┐
│                   │  │                    │   │                   │
│                   │  │                    │   │                   │
│<<Bounded Context>>│  │      <<AMQP>>      │   │<<Bounded Context>>│
│   GalaticEmpire   │─▶│   Message broker   │◀──│   RebelAlliance   │
│                   │  │                    │   │                   │
│                   │  │                    │   │                   │
└───────────────────┘  └────────────────────┘   └───────────────────┘
```

The message brokering platform will contain a few supporting services besides the routing component itself:

- A `reconstitution service proxy` responsible for delegating requests for reconstitution to the producing bounded context
- A `schema registry` serving as documentation for the published language sent over the broker (this is currently just a repo folder (/schemas) with JSON schemas)
- A `message deferrer service` for scheduled future delivery of a message. Vendor-specific AMQP-extensions for this exists, but as the samples try to stay as clean to the OASIS-definition as possible, these extensions are not implemented

```
┌─────────────────┐ ┌─────────────────┐ ┌────────────────┐
│                 │ │                 │ │                │
│                 │ │                 │ │                │
│ Reconstitution  │ │ Schema registry │ │Message Deferrer│
│  service proxy  │ │                 │ │    service     │
│                 │ │                 │ │                │
│                 │ │                 │ │                │
└─────────────────┘ └─────────────────┘ └────────────────┘
┌────────────────────────────────────────────────────────┐
│                                                        │
│                                                        │
│                        <<AMQP>>                        │
│                     Message broker                     │
│                                                        │
│                                                        │
└────────────────────────────────────────────────────────┘
```

# How can I contribute?

That's easy, just write me on larslb@thinkability.dk or open an issue in this repo :)

All contributions are very welcome!
