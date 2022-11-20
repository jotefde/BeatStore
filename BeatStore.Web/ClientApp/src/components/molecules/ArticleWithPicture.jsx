import React from 'react';
import cx from 'classnames';
import { Container, Row, Col } from 'react-bootstrap';
import { PublicImage } from 'components/atoms';
import { renderMarkup } from 'react-render-markup';

const ArticleWithPicture = ({ data, isTextOnLeft, titleImage, className, children, ...props }) => {
  const textLastOrder = isTextOnLeft ? 'last' : 'first';
  const textFirstOrder = isTextOnLeft ? 'first' : 'last';
  const imageColSetting = {
    md: { span: 6, order: textLastOrder },
    xs: { span: 12, order: textLastOrder },
  };
  const textColSetting = {
    md: { span: 6, order: textFirstOrder },
    xs: { span: 12, order: textFirstOrder },
  };
  const { id, picture, title } = data ?? { id: null, picture: null, title: null };

  return (
    <article data-article={ id } className={cx('articleWithPicture', className)} {...props}>
      <Container>
        <Row>
          <Col {...imageColSetting}>
            <PublicImage relSrc={ `articles/${picture}` } className={ 'articleWithPicture__image' } alt={ title }/>
          </Col>

          <Col as={'div'} {...textColSetting }>
            <header className={ 'articleWithPicture__header' }>
              { titleImage && <PublicImage relSrc={ titleImage } alt={ title }/> }
              <h2>{ renderMarkup(title) }</h2>
              <hr className={ 'articleWithPicture__underline' }/>
            </header>
            <div className={ 'articleWithPicture__content' }>{ children }</div>
          </Col>
        </Row>
      </Container>
    </article>
  );
};

export default ArticleWithPicture;