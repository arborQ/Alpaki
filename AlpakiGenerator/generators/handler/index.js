var Generator = require('yeoman-generator');

module.exports = class extends Generator {
    constructor(args, opts) {
        super(args, opts);
        this.option("module", { type: String, required: true, default: "MotoLogic" });
        this.option("name", { type: String, required: true });
    }
    // method1() {
    //     this.log('method 1 just ran');
    // }
    async prompting() {
        this.answers = await this.prompt([
            // {
            //     type: "input",
            //     name: "controler",
            //     message: "Add as controller action?",
            //     default: `${this.options.name}Controller` // Default to current folder name
            // },
            {
                type: 'checkbox',
                name: 'module',
                message: 'Select module',
                choices: [
                    { name: 'Dream', value: 'Dream' },
                    { name: 'Moto', value: 'Moto' },
                ]
            },
            {
                type: 'checkbox',
                name: 'context',
                message: 'Select Db context',
                choices: [
                    { name: 'IDatabaseContext', value: 'IDatabaseContext' },
                    { name: 'IMotoDatabaseContext', value: 'IMotoDatabaseContext' },
                ]
            },
            {
                type: "confirm",
                name: "unit",
                message: "Would you like to create unit tests?"
            }
        ]);

        // this.log("app name", answers.name);
        // this.log("cool feature", answers.cool);
    }
    // C:\Users\lukasz.wojcik\Alpaki\backend\Alpaki.MotoLogic\Handlers\
    writing() {
        if (!this.options.name) {
            throw 'Name is required';
        }
        console.log({ ...this.answers });

        const answerOptions = {
            module : this.answers.module[0],
            context : this.answers.context[0]
        };
        this.fs.copyTpl(
            this.templatePath('handler'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${this.options.name}/${this.options.name}Handler.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('request'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${this.options.name}/${this.options.name}Request.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('response'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${this.options.name}/${this.options.name}Response.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('validator'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${this.options.name}/${this.options.name}RequestValidator.cs`),
            { name: this.options.name, ...answerOptions }
        );

        if (this.answers.unit) {
            this.fs.copyTpl(
                this.templatePath('unit'),
                this.destinationPath(`Alpaki.${answerOptions.module}.UnitTests/Handlers/${this.options.name}HandlerTests.cs`),
                { name: this.options.name, ...answerOptions }
            );
        }
    }
};