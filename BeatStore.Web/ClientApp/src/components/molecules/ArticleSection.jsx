import React from 'react';
import cx from 'classnames';
import { renderMarkup } from 'react-render-markup';
import { PublicImage } from 'components/atoms';
import { Container } from 'react-bootstrap';

const ArticleSection = ({
                          data,
                          underlined = false,
                          titlePosition = 'left',
                          contentPosition = 'justify',
                          titleImage,
                          children, className, ...props }) => {
  const headerClassName = cx('articleSection__header', `articleSection__header--${titlePosition}`)
  const contentClassName = cx('articleSection__content', `articleSection__content--${contentPosition}`)
  const { id, title } = data ?? { id: null, title: null };
  return (
    <article data-article={id} className={ cx('articleSection', className) } {...props}>
      <Container>
        <header className={headerClassName}>
          { titleImage && <PublicImage relSrc={ titleImage } alt={ title }/> }
          <h2>{ renderMarkup(title) }</h2>
          { props.subtitle ?? <></> }
          { underlined && <hr className={'articleSection__underline'} /> }
        </header>
        <div className={contentClassName}>
          { children }
        </div>
      </Container>
    </article>
  );
};

export default ArticleSection;