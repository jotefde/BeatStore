import React from 'react';
import { Table } from 'react-bootstrap';

const NutritionalTable = ({ kcal, fat, saturatedFats, carbs, sugars, proteins, salt }) => {
  return (
    <section className={'nutritionalTable'}>
      <Table striped bordered hover>
        <tbody>
          <tr>
            <th>{'Wartość odżywcza w 100g'}</th>
            <td>{kcal}</td>
          </tr>
          <tr>
            <th>{'Tłuszcz'}</th>
            <td>{fat}</td>
          </tr>
          <tr>
            <th>&emsp;{'w tym nasycone kwasy tłuszczowe'}</th>
            <td>{saturatedFats}</td>
          </tr>
          <tr>
            <th>{'Węglowodany'}</th>
            <td>{carbs}</td>
          </tr>
          <tr>
            <th>&emsp;{'w tym cukry'}</th>
            <td>{sugars}</td>
          </tr>
          <tr>
            <th>{'Białko'}</th>
            <td>{proteins}</td>
          </tr>
          <tr>
            <th>{'Sól'}</th>
            <td>{salt}</td>
          </tr>
        </tbody>
      </Table>
    </section>
  );
};

export default NutritionalTable;