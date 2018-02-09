class Card extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.card };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        var res = '<tr id="{this.state.data.id}">';
        if (this.state.data.complete == 100) {
            res = res + '<td></td><td></td><td><b>{this.state.data.title}</b></td>';
        }
        else if (this.state.data.complete >= 0 && this.state.data.complete < 100) {
            res = res + '<td></td><td><b>{this.state.data.title}</b><progress value="{this.state.data.complete}" max="100"></progress></td><td></td>';
        }
        else {

        }
        return
        <tr id="{this.state.data.id}">
                    <td>Имя: <b>{this.state.data.title}</b></td>
                    <td>Дата старта: {this.state.data.start}</td>
                    <td>Дата окончания: {this.state.data.end}</td>
                    <td>Состояние: {this.state.data.complete}</td>
                    <td><button onClick={this.onClick}>Удалить</button></td>
        </tr>;
    }
}


class CardForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { title: "", description: "", complete: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onTitleChange = this.onTitleChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onEndChange = this.onEndChange.bind(this);
        this.onCompleteChange = this.onCompleteChange.bind(this);
    }
    onTitleChange(e) {
        this.setState({ title: e.target.value });
    }
    onDescriptionChange(e) {
        this.setState({ description: e.target.value });
    }
    onEndChange(e) {
        this.setState({ end: e.target.value });
    }
    onCompleteChange(e) {
        this.setState({ complete: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var cardName = this.state.title.trim();
        var cardDescription = this.state.description.trim();
        var cardEnd = this.state.end;
        var cardComplete = this.state.complete;
        if (!cardName || cardComplete < 0 || cardEnd < Date.now) {
            return;
        }
        this.props.onCardSubmit({ id: cardId, title: cardName, description: cardDescription, end: cardEnd, complete: cardComplete });
        this.setState({ title: "", description: "", end: "", complete: "" });
    }
    render() {
        return (
          <form onSubmit={this.onSubmit}>
              <fieldset>
                  Карточка
              <p>
                  <input type="text"
                         placeholder="Имя"
                         value={this.state.title}
                         onChange={this.onTitleChange} />
              </p>
              <p>
                  <input type="text"
                         placeholder="Описание"
                         value={this.state.description}
                         onChange={this.onDescriptionChange} />
              </p>
              <p>
                  <input type="datetime-local"
                         placeholder="Конечная дата"
                         value={this.state.end}
                         onChange={this.onEndChange } />
              </p>
            <p>
                <input type="number"
                       placeholder="Прогрес"
                       value={this.state.complete}
                       onChange={this.onCompleteChange} />
            </p>
              </fieldset>
            <input type="submit" value="Сохранить" />

          </form>
        );
    }
}


class CardsList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { cards: [] };

        this.onAddCard = this.onAddCard.bind(this);
        this.onEditCard = this.onEditCard.bind(this);
        this.onRemoveCard = this.onRemoveCard.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ cards: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // добавление объекта
    onAddCard(card) {
        if (card) {

            var data = JSON.stringify({ "title": card.title, "description": card.description, "end": card.end });
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    // редактирование объекта
    onEditCard(card) {
        if (card) {

            var data = JSON.stringify({ "id": card.id, "title": card.title, "description": card.description, "end": card.end, "complete": card.complete });
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    // удаление объекта
    onRemoveCard(card) {

        if (card) {
            var url = this.props.apiUrl + "/" + card.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }
    render() {

        var remove = this.onRemoveCard;
        return <div>
                <h2>Список задач</h2>
                <div>
                    {
                        this.state.cards.map(function (card) {

                            return <Card key={card.id} card={card} onAdd onRemove={remove} />
                        })
                    }
                </div>
                <hr />
                <footer>
                    <p>{Date} - SimpleKanban</p>
                </footer>
            </div>;
    }
}

ReactDOM.render(
  <CardsList apiUrl="/api/home" />,
  document.getElementById("content")
);
