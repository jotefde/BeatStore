import React, { Component } from 'react';
import { Container } from 'react-bootstrap';
import { TrackGrid } from 'components/organisms';

export class HomePage extends Component {
    static displayName = HomePage.name;

  render() {
    return (
        <Container as={'section'} id={'Tracks'} className={'pageContent'}>
            <h1 className={'pageTitle'}>{'Tracks'}</h1>
            <TrackGrid/>
        </Container>

    );
  }
}
